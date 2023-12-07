import re
import json
import os
import pandas as pd
import pyodbc
from pandas import json_normalize
import datetime
import sys

if len(sys.argv) < 2:
    print("Faltan argumentos. Debe proporcionar la ruta del archivo .txt a procesar.")
else:
    origin=sys.argv[1]
    print(f"Procesando el archivo: {origin}")

    #Archivos origen (txt) y destino (xlsx)
    dest=os.path.splitext(origin)[0]+".xlsx"

    ##########################################################################################################################

    #Conexión a base de datos POSEIDON
    server = 'localhost'
    database = 'Prueba'

    #Definición cadena de conexión
    conn_str = f'DRIVER={{ODBC Driver 17 for SQL Server}};SERVER={server};DATABASE={database};Trusted_Connection=yes;'

    #Establecer conexión a base de datos
    conn= pyodbc.connect(conn_str)

    #Creación del cursor
    cursor=conn.cursor()

    # Definir una función para insertar los datos en la base de datos
    def insert_data(table_name, dataframe):
        dataframe.to_sql(table_name, conn, if_exists='replace', index=False)

    ##########################################################################################################################

    #Array de Strings con cada uno de los objetos a parsear
    array=["monitor","node","pool","virtual","irule"]

    #Open the configuration file
    with open(origin,'r') as f:
        config = f.read()

    #Por cada objecto a parsear...
    for object in array:
        #Si el objeto es diferente de irule y de monitor...
        if (object != "irule" and object!="monitor"):
            #Patrón para localizar y capturar el nombre del objeto dentro del archivo
            pattern = re.compile(r'ltm '+object+' ([^ ]+)')
        #Si el objeto SÍ que es un irule...
        elif (object=="irule"):
            #Patrón para localizar y capturar el nombre del irule
            pattern = re.compile(r'ltm rule ([^ ]+)')
        #Si el objeto es un monitor...
        elif (object=='monitor'):
            #Patrón para localizar y capturar el nombre del monitor dentro del archivo
            pattern=re.compile(r'ltm monitor ([^{]+)')
        
        #Array para guardar las coincidencias de cada objeto
        objects = pattern.findall(config)
        #Definción de lista para agregar diccionarios más adelante
        list=[]
        i=0
        #Por cada subobjeto dentro del mismo tipo de objetos...
        for subobject in objects: 
            #Si el objeto es un nodo...
            if object=="node":
                #Cuando la i declarada anteriormente vale 0 (significa que estamos tratrando con un tipo de objeto distinto)...
                if i==0:
                    #Patrón para localizar y capturar la IP de cada uno de los nodos
                    pattern2=re.compile(r'ltm node\s+/\w+\/[\w\d._-]+\s+{\s+address\s+([\d.]+)')
                    #Patrón para localizar y capturar la descripción de cada uno de los nodos
                    pattern3=re.compile(r'ltm node\s+/\w+\/[\w\d._-]+\s+{\s+address\s+[\d.]+(?:\s+description\s+((?:"[^"]+"|\S+)))?')
                    #Array para guardar todas las descripciones de cada uno de los nodos
                    descs=pattern3.findall(config)
                    #Array para guardar todas las IPs de cada uno de los nodos
                    IPs=pattern2.findall(config)
                #Declaración de diccionario con campos de nodo
                dict= {"Nodo": subobject,
                    "IP": IPs[i],
                    "Descripción": descs[i]}
                #Agregar el diccionario a la lista declarada anteriormente            
                list.append(dict)
                #Incrementar la i para que con el siguiente subobjeto coja sus respectivos valores (IP, descripción)
                i=i+1
            #Si el objeto es una pool...
            elif object=="pool":
                #Patrón para localizar y capturar los mimebros de la pool
                pattern5=re.compile(r'ltm pool '+subobject+'\s+{\s+(?:description\s+(?:"[^"]+"|\S+))?\s+(?:load-balancing-mode\s+[^\s]+)?\s+(?:members\s+{\s+(([^\s}]+)(?:[^}]+}\s*)*)*})?')
                #Array para separar cada uno de los miembros en posiciones independientes
                coincidencias=re.search(pattern5, config)
                #Si existen coinicidencias...
                if coincidencias:
                    #En caso de que las coincidencias estén vacías hay que capturar y controlar el error
                    try:
                        #Array para almacenar los miembros de cada pool
                        members=re.findall(r'/Common/([\w\d._-]+):', coincidencias.group(1))
                        #Array para almacenar los puertos de cada miembro de cada pool
                        puertos=re.findall(r'/Common/[\w\d._-]+:([^\s]+)', coincidencias.group(1))
                        #Array para almacenar las direcciones IP de cada uno de los miembros de cada pool
                        direcciones=re.findall(r'/Common/[\w\d._-]+:[^\s]+\s+{\s+(?:address\s+([^\s]+))?', coincidencias.group(1))
                        #Array para almacenar las descripciones de cada uno de los mimebros de cada pool
                        descripciones=re.findall(r'/Common/[\w\d._-]+:[^\s]+\s+{\s+(?:address\s+[^\s]+)?\s+(?:description\s+((?:"[^"]+"|\S+)))?', coincidencias.group(1))
                    #Cuando no hay coincidencias salta una excepción...
                    except:
                        #Se ponen todos los arrays referentes a los miembros con un sólo String vacío para solucionar el error.
                        members=[""]
                        puertos=[""]
                        direcciones=[""]
                        descripciones=[""]
                #Si la i es igual a 0 (significa que estamos tratrando con un tipo de objeto distinto)...
                if i==0:
                    #Patrón para localizar y capturar cada una de las descripciones de cada una de las pools
                    pattern2=re.compile(r'ltm pool\s+/\w+\/[\w\d._-]+\s+{\s+(?:description\s+((?:"[^"]+"|\S+)))?')
                    #Patrón para localizar y capturar cada uno de los balancing-mode de cada una de las pools
                    pattern3=re.compile(r'ltm pool\s+/\w+\/[\w\d._-]+\s+{\s+(?:description\s+(?:"[^"]+"|\S+))?\s+(?:load-balancing-mode\s+([^\s]+))?')
                    #Patrón para localizar y capturar cada uno de los monitores de cada una de las pools
                    pattern4=re.compile(r'ltm pool\s+/\w+\/[\w\d._-]+\s+{\s+(?:description\s+(?:"[^"]+"|\S+))?\s+(?:load-balancing-mode\s+[^\s]+)?\s+(?:members\s+{(?:[^}]+}\s*)*})?\s+(?:monitor\s+([^\s]+))?')
                    #Array para almacenar cada una de las descripciones de cada una de las pools
                    descs=pattern2.findall(config)
                    #Array para almacenar cada uno de los balancing-mode de cada una de las pools
                    mode=pattern3.findall(config)
                    #Array para almacenar cada uno de los monitores de cada una de las pools
                    monitor=pattern4.findall(config)
                x=0
                #Por cada mimebro dentro del array de miembros...
                for member in members:
                    #Creamos un diccionario con los siguientes campos
                    dict={"Pool": subobject,
                        "Descripción (Pool)": descs[i],
                        "Tipo Balanceo (Pool)": mode[i],
                        "Nombre (Miembro):": member,
                        "Puerto (Miembro):": puertos[x],
                        "Dirección (Miembro):": direcciones[x],
                        "Descripción (Miembro):": descripciones[x],
                        "Monitor (Pool)": monitor[i]
                        }
                    #Se incrementa el valor de x para asignar correctamente el miembro correspondiente a cada diccionario
                    x=x+1
                    #Se añade el diccionario a la lista declarada anteriormente
                    list.append(dict)
                #Se incremente el valor de i para asignar correctamente la pool correspondiente a cada diccionario
                i=i+1
            elif object=='monitor':
                if i==0:
                    pattern2=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+([^\s]+))?')
                    adaptive=pattern2.findall(config)
                    pattern6=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+([^\s]+))?')
                    cipherlist=pattern6.findall(config)
                    pattern7=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+([^\s]+))?')
                    compatibility=pattern7.findall(config)
                    pattern14=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+([^\s]+))?')
                    debug=pattern14.findall(config)
                    pattern3=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+([^\s]+))?')
                    defaults_from=pattern3.findall(config)
                    pattern4=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+((?:"[^"]+"|\S+)))?')
                    descs=pattern4.findall(config)
                    #http /Common/Casiopeams_ElectronicPrivatePrescription.Service_Priv
                    pattern5=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+([^\s]+))?')
                    dests=pattern5.findall(config)
                    pattern15=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+([^\s]+))?')
                    get=pattern15.findall(config)
                    pattern8=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+([^\s]+))?')
                    interval=pattern8.findall(config)
                    pattern16=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+([^\s]+))?')
                    password=pattern16.findall(config)
                    pattern17=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+([^\s]+))?')
                    server=pattern17.findall(config)
                    pattern18=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+([^\s]+))?')
                    service=pattern18.findall(config)
                    pattern8=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+([^\s]+))?')
                    ip_dscp=pattern8.findall(config)
                    pattern9=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+((?:"[^"]+"|\S+)))?')
                    recv=pattern9.findall(config)
                    pattern10=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+(?:"[^"]+"|\S+))?(?:\s+recv-disable\s([^\s]+))?')
                    recv_disable=pattern10.findall(config)
                    pattern20=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+(?:"[^"]+"|\S+))?(?:\s+recv-disable\s[^\s]+)?(?:\s+reverse\s+([^\s]+))?')
                    reverse=pattern20.findall(config)
                    pattern11=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+(?:"[^"]+"|\S+))?(?:\s+recv-disable\s[^\s]+)?(?:\s+reverse\s+[^\s]+)?(?:\s+send\s+((?:"[^"]+"|\S+)))?')
                    send=pattern11.findall(config)
                    pattern21=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+(?:"[^"]+"|\S+))?(?:\s+recv-disable\s[^\s]+)?(?:\s+reverse\s+[^\s]+)?(?:\s+send\s+(?:"[^"]+"|\S+))?(?:\s+ssl-profile\s+([^\s]+))?')
                    ssl_profile=pattern21.findall(config)
                    pattern12=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+(?:"[^"]+"|\S+))?(?:\s+recv-disable\s[^\s]+)?(?:\s+reverse\s+[^\s]+)?(?:\s+send\s+(?:"[^"]+"|\S+))?(?:\s+ssl-profile\s+[^\s]+)?(?:\s+time-until-up\s([^\s]+))?')
                    untilup=pattern12.findall(config)
                    pattern13=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+(?:"[^"]+"|\S+))?(?:\s+recv-disable\s[^\s]+)?(?:\s+reverse\s+[^\s]+)?(?:\s+send\s+(?:"[^"]+"|\S+))?(?:\s+ssl-profile\s+[^\s]+)?(?:\s+time-until-up\s[^\s]+)?(?:\s+timeout\s+([^\s]+))?')
                    timeout=pattern13.findall(config)
                    pattern19=re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+(?:"[^"]+"|\S+))?(?:\s+recv-disable\s[^\s]+)?(?:\s+reverse\s+[^\s]+)?(?:\s+send\s+(?:"[^"]+"|\S+))?(?:\s+ssl-profile\s+[^\s]+)?(?:\s+time-until-up\s[^\s]+)?(?:\s+timeout\s+[^\s]+)?(?:\s+username\s+([^\s]+))?')
                    username=pattern19.findall(config)
                #Se agrega al diccionario simplemente el tipo de objeto y su nombre
                dict = {object: subobject,
                        "Adaptive":adaptive[i],
                        "Cipherlist": cipherlist[i],
                        "Compatibility": compatibility[i],
                        "Debug": debug[i],
                        "Defaults_from": defaults_from[i],
                        "Description": descs[i],
                        "Destination": dests[i],
                        "get": get[i],
                        "Interval": interval[i],
                        "password": password[i],
                        "server": server[i],
                        "service": service[i],
                        "IP_DSCP": ip_dscp[i],
                        "RECV": recv[i],
                        "RECV_DISABLE": recv_disable[i],
                        "reverse": reverse[i],
                        "SEND": send[i],
                        "ssl_profile": ssl_profile[i],
                        "time_until_up": untilup[i],
                        "timeout": timeout[i],
                        "username": username[i]
                        }
                #Se agrega el diccionario a la lista declarada anteriormente
                list.append(dict)
                i=i+1
            elif object=='virtual':
                if i==0:
                    pattern2=re.compile(r'ltm virtual(?:.*?)(?=ltm virtual|$|pool\s)(?:pool\s+([^\s]+))?', re.DOTALL)
                    assignedPool=pattern2.findall(config)
                pattern3=re.compile(r'ltm virtual '+subobject+'(?:.*?)(?=ltm virtual|$|rules\s)(?:rules\s+(.*?)(?=}))?', re.DOTALL)
                coincidencias=re.search(pattern3, config)
                try:  
                    rules=re.findall(r'\s+([^\s]+)', coincidencias.group(1))
                except:
                    rules=[""]
                for rule in rules:
                    dict={object: subobject,
                        "POOL": assignedPool[i],
                        "Rule": rule}
                    list.append(dict)
                i=i+1
            #Si el objeto no es ni un nodo ni una pool...
            else:
                pattern2=re.compile(r'ltm rule '+subobject+' (?:.*?)(?=ltm rule|if\s)(?:(.*?))?(?=}}|ltm rule)', re.DOTALL)
                #(?:.*?)(?=ltm rule|$|if\s)(?:if\s*{\s*[(.*?)(?=}))?
                coincidencias=re.search(pattern2,config)
                coinicidencias2=pattern2.findall(config)
                if coincidencias:
                    pools=re.findall(r'if[^{]+[^}]+}\s+(?:(?:.*?)(?=pool|node|virtual)((?:pool|node|virtual)\s+[^\s]+))?',coincidencias.group(1), re.DOTALL)
                    condiciones=re.findall(r'if[^{]+([^}]+})',coincidencias.group(1))
                    x=0
                    for pool in pools:
                        dict = {"Rule": subobject,
                        "Irule": condiciones[x],
                        "Redireccionamiento":pool}
                        x=x+1
                        #Se agrega el diccionario a la lista declarada anteriormente
                        list.append(dict)
                #pattern3=re.compile(r'ltm rule '+subobject+' (?:.*?)(?=pool\s)(?:pool\s+([^\s]+))?(?=ltm rule|}})', re.DOTALL)
                #conditions2=pattern3.findall(config)
                #Se agrega al diccionario simplemente el tipo de objeto y su nombre
                
        #Abrimos el json correspondiente de cada tipo de objeto en modo escritura (si no existe, se crea)...
        with open(object+'s.json','w') as f:
            #Se vuelca el contenido de la lista que se ha ido complementado todo este tiempo en formato json 
            json.dump(list,f,indent=4)
        #Si todo va bien, nos avisa por consola que el archivo .json se ha creado correctamente
        print(object+"s.json creado correctamente")
        #Abrimos el .json recién creado...
        with open (object+'s.json') as data_file:
            #Obetenemos los objetos json del arhivo .json
            _datafile= json.load(data_file)
            #Convertimos cada una de las declaraciones en una linea de un formulario tabulado
            df=json_normalize(_datafile)
            #Pasamos las tabulaciones creadas en el paso anterior a un archivo .csv
            df.to_csv(object+"s.csv", index=False)    
        
        #Si todo va bien, nos avisa por consola que se ha creado el archivo .csv correctamente.
        print(object+"s.csv creado correctamente")

    ###################################################################################################################

        if object == 'monitor':
        #Pasar csv a Base de datos Poseidón
            data = pd.read_csv('monitors.csv')

            data= df.where(pd.notna(df), None)

            df= pd.DataFrame(data)
            
            for row in df.itertuples():
                cursor.execute('''
                                INSERT INTO Monitors (Active, Adaptive, Cipherlist, Compatibility, DateInsert, Debug, Defaults_from, Description, Destination, IP_DSCP, Interval, Name, Password, RECV, RECV_disable, Reverse, SEND, Server, Service, get, ssl_profile, time_until_up, timeout, username)
                                VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)''',
                                True,
                                row.Adaptive,
                                row.Cipherlist,
                                row.Compatibility,
                                datetime.datetime.now(),
                                row.Debug,
                                row.Defaults_from,
                                row.Description,
                                row.Destination,
                                row.IP_DSCP,
                                row.Interval,
                                row.monitor,
                                row.password,
                                row.RECV,
                                row.RECV_DISABLE,
                                row.reverse,
                                row.SEND,
                                row.server,
                                row.service,
                                row.get,
                                row.ssl_profile,
                                row.time_until_up,
                                row.timeout,
                                row.username
                                )
        elif (object=='pool'):
            data = pd.read_csv('pools.csv')

            data= df.where(pd.notna(df), None)

            df= pd.DataFrame(data)

            # Itera a través de las filas del DataFrame
            for row in df.itertuples():
                monitor_pool_index = df.columns.get_loc('Monitor (Pool)')
                monitor_pool = row[monitor_pool_index+1]  # Accede por índice

                # Realiza una consulta SQL para buscar el MonitorId en la tabla Monitors
                query = "SELECT MonitorId FROM Monitors WHERE Name LIKE ?"
                cursor.execute(query, f'%{monitor_pool} ')
                
                # Recupera el MonitorId desde la consulta
                monitor_id = cursor.fetchone()

                print(monitor_id)
                            
                # Asigna el MonitorId al DataFrame
                if monitor_id:
                    df.at[row.Index, 'MonitorId'] = monitor_id[0]
                    print(monitor_id[0])
                else:
                    df.at[row.Index, 'MonitorId'] = None
                    print(df.at[row.Index, 'MonitorId'])

                # Reemplazar "nan" por None utilizando pd.notna
                df['MonitorId'] = df['MonitorId'].where(pd.notna(df['MonitorId']), None)

            for row in df.itertuples():
                balancer_type_index = df.columns.get_loc('Tipo Balanceo (Pool)')
                description_index = df.columns.get_loc('Descripción (Pool)')
                pool_name = row.Pool

                # Verifica si ya existe un registro con el mismo valor en la columna 'Name'
                cursor.execute("SELECT COUNT(*) FROM Pools WHERE Name = ?", pool_name)
                result = cursor.fetchone()

                if result and result[0] > 0:
                    print(f"Registro duplicado para: {pool_name}")
                else:
                    cursor.execute('''
                        INSERT INTO Pools (MonitorId, Active, BalancerType, DateInsert, Description, Name)
                        VALUES (?,?,?,?,?,?)''',
                        df.at[row.Index, 'MonitorId'],
                        True,
                        row[balancer_type_index+1],
                        datetime.datetime.now(),
                        row[description_index+1],
                        pool_name)

                name_member=df.columns.get_loc('Nombre (Miembro):')

                cursor.execute("SELECT COUNT(*) FROM Nodes WHERE Name=?",row[name_member+1])
                result2=cursor.fetchone()

                if result2 and result2[0]>0:
                    print(f"Registro duplicado para: {row[name_member+1]}")
                else:
                    descritpion_node=df.columns.get_loc('Descripción (Miembro):')
                    direction_member=df.columns.get_loc('Dirección (Miembro):')
                    
                    port_member=df.columns.get_loc('Puerto (Miembro):')
                    cursor.execute('''
                                    INSERT INTO Nodes ( Active, DateInsert, Description,IP, Name, Port)
                                    VALUES (?,?,?,?,?,?)''',
                                    True,  
                                    datetime.datetime.now(),                     
                                    row[descritpion_node+1],
                                    row[direction_member+1],
                                    row[name_member+1],
                                    row[port_member+1])  

                cursor.execute("SELECT COUNT(*) FROM NodePool WHERE Name=?",row[name_member+1]+'----'+pool_name)
                result3=cursor.fetchone()

                if result3 and result3[0]>0:
                    print(f"Registro duplicado para: {row[name_member+1]+'----'+pool_name}")
                else:
                    cursor.execute("SELECT NodeId FROM Nodes WHERE Name = ?", row[name_member+1])
                    memberId=cursor.fetchone()
                    cursor.execute("SELECT PoolId FROM Pools WHERE Name = ?", pool_name)
                    poolId=cursor.fetchone()
                    cursor.execute('''
                                    INSERT INTO NodePool (NodeId, PoolId, Active, DateInsert, Name)
                                    VALUES (?,?,?,?,?)''',
                                    memberId[0],
                                    poolId[0],
                                    True,
                                    datetime.datetime.now(),
                                    row[name_member+1]+'----'+pool_name
                                    ) 
        elif(object=='virtual'):
            data = pd.read_csv('virtuals.csv')

            data= df.where(pd.notna(df), None)

            df= pd.DataFrame(data)

            for row in df.itertuples():
                cursor.execute("SELECT COUNT(*) FROM Virtuals WHERE Name=?",row.virtual)
                result4=cursor.fetchone()

                if result4 and result4[0]>0:
                    print(f"Registro duplicado para: {row.virtual}")
                else:
                        cursor.execute("SELECT PoolId FROM Pools WHERE Name=?", row.POOL)
                        poolId=cursor.fetchone()

                        # Asigna el valor de PoolId a None si poolId es nulo
                        PoolId = poolId[0] if poolId is not None else None
                        cursor.execute('''
                                        INSERT INTO Virtuals (Active, DateInsert, Name, PoolId)
                                        VALUES (?,?,?,?)''',
                                        True,
                                        datetime.datetime.now(),
                                        row.virtual,
                                        PoolId
                                        )
                        
                cursor.execute("SELECT COUNT(*) FROM Rules WHERE Name=?",row.Rule)
                result5=cursor.fetchone()

                if (result5 and result5[0]>0) or row.Rule=='':
                    print(f"Registro duplicado para: {row.Rule}")
                else:
                    cursor.execute("SELECT VirtualId FROM Virtuals WHERE Name=?",row.virtual)
                    virtualId=cursor.fetchone()
                    cursor.execute('''
                                    INSERT INTO Rules (Active, DateInsert, Name, VirtualId)
                                    VALUES (?,?,?,?)''',
                                    True,
                                    datetime.datetime.now(),
                                    row.Rule,
                                    virtualId[0]
                                    )   
                
        elif (object=='irule'):
            data = pd.read_csv('irules.csv')

            data= df.where(pd.notna(df), None)

            df= pd.DataFrame(data)

            for row in df.itertuples():
                cursor.execute("SELECT COUNT(*) FROM Rules WHERE Name=?",row.Rule)
                result7=cursor.fetchone()

                if (result7 and result7[0]>0) or row.Rule=='':
                    print(f"Registro duplicado para: {row.Rule}")
                else:
                    cursor.execute('''
                                    INSERT INTO Rules (Active, DateInsert, Name, VirtualId)
                                    VALUES (?,?,?,?)''',
                                    True,
                                    datetime.datetime.now(),
                                    row.Rule,
                                    None
                                    )   

            for row in df.itertuples():
                cursor.execute("SELECT RuleId FROM Rules WHERE Name=?", row.Rule)
                ruleId=cursor.fetchone()

                cursor.execute("SELECT COUNT(*) FROM Irules WHERE Name=? AND RuleId =?",(row.Irule, ruleId[0]))
                result6=cursor.fetchone()

                if result6 and result6[0]>0:
                    print(f"Registro duplicado para: {row.Irule}")
                else:
                    print(row.Redireccionamiento)
                    cursor.execute('''
                                    INSERT INTO Irules (Active, DateInsert, Name, Redirect, RuleId)
                                    VALUES (?,?,?,?,?)''',
                                    True,
                                    datetime.datetime.now(),
                                    row.Irule,
                                    row.Redireccionamiento,
                                    ruleId[0]
                                    )

    ###################################################################################################################


        #Eliminar jsons para limpiar (comentar en caso de querer mantener los .json)
        file=object+'s.json'
        if (os.path.exists(file) and os.path.isfile(file)):
            os.remove(file)
            print(object+('s.json deleted'))

    # Lista de nombres de archivos CSV a combinar
    archivos_csv = ["nodes.csv", "pools.csv", "irules.csv", "virtuals.csv", "monitors.csv"]

    # Crear un DataFrame para cada archivo CSV y almacenarlos en un diccionario
    dataframes = {}
    for archivo in archivos_csv:
        nombre_hoja = archivo.split(".")[0]  # Usaremos el nombre del archivo como nombre de la hoja
        dataframes[nombre_hoja] = pd.read_csv(archivo)

        #Eliminar .csv para limpiar (comentar en caso de querer mantener los .csv)
        if (os.path.exists(archivo) and os.path.isfile(archivo)):
            os.remove(archivo)
            print(archivo+' deleted')

    # Escribir los DataFrames en un archivo de Excel con hojas separadas 
    with pd.ExcelWriter(dest) as writer:
        for hoja, df in dataframes.items():
            df.to_excel(writer, sheet_name=hoja, index=False)

            # Obtener la hoja actual del archivo de Excel
            hoja_actual = writer.sheets[hoja]
            
            # Ajustar el tamaño de las celdas al contenido más largo en cada columna
            for columna_cells in hoja_actual.columns:
                longitud_maxima = 0
                for cell in columna_cells:
                    try:
                        if len(str(cell.value)) > longitud_maxima:
                            longitud_maxima = len(cell.value)
                    except TypeError:
                        pass
                ajuste = (longitud_maxima + 2) * 1.2  # Factor de ajuste para dar un poco de espacio adicional
                hoja_actual.column_dimensions[columna_cells[0].column_letter].width = ajuste

    print(f"Se han combinado los archivos en {dest} con hojas separadas.")

    # Confirmar los cambios y cerrar la conexión
    conn.commit()
    conn.close()

    print("Datos guardados en la Base de Datos")