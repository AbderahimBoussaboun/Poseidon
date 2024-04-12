import re
import json
import os
import pandas as pd
import pyodbc
from pandas import json_normalize
import datetime
import sys
import traceback
import logging

try:
    if len(sys.argv) < 4:
        print("Faltan argumentos. Debe proporcionar la ruta del archivo .txt a procesar, el nombre del balanceador y el mensaje de commit.")
    else:
        origin = sys.argv[1]
        # Segundo argumento: Nombre del balanceador
        balancer_name = sys.argv[2]
        # Tercer argumento: Mensaje de commit
        commit_message = sys.argv[3]
        print(
            f"Procesando el archivo: {origin} para el balanceador: {balancer_name}")

        # Archivos origen (txt) y destino (xlsx)
        dest = os.path.splitext(origin)[0]+".xlsx"

        ##########################################################################################################################

        # Conexión a base de datos POSEIDON
        server = 'localhost'
        database = 'Prueba2'

        # Definición cadena de conexión
        conn_str = f'DRIVER={{ODBC Driver 17 for SQL Server}};SERVER={server};DATABASE={database};Trusted_Connection=yes;'

        # Establecer conexión a base de datos
        conn = pyodbc.connect(conn_str)

        # Creación del cursor
        cursor = conn.cursor()

        cursor.execute(
            "SELECT BalancerId FROM Balancers WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?", balancer_name)
        balancer_id = cursor.fetchone()[0]

        def disable_relationships(df, df_column, table_name, id_column):
            """
            Deshabilita las relaciones de las entidades en la tabla especificada y registra el historial de cambios.

            Args:
                cursor: Cursor de base de datos para ejecutar consultas.
                df (DataFrame): DataFrame que contiene los datos del archivo introducido.
                df_column (str): Nombre de la columna en el DataFrame que contiene los nombres de las entidades.
                table_name (str): Nombre de la tabla de la base de datos que contiene las entidades.
                id_column (str): Nombre de la columna de ID en la tabla de la base de datos.
                balancer_id (int): ID del balanceador relacionado.
            """
            # Eliminar la 's' final del nombre de la tabla si está presente
            if table_name.endswith('s'):
                table_name = table_name[:-1]

            # Obtener todos los nodos existentes en la base de datos
            cursor.execute(f"SELECT {id_column}, Name FROM {table_name.lower()}s INNER JOIN Balancer{table_name} ON {table_name.lower()}s.{id_column}=Balancer{table_name}.{table_name.lower()}_id")
            existing_entities = cursor.fetchall()

            entities_to_disable = []

            # Deshabilitar las relaciones en la tabla de balanceadores y registrar el historial de cambios
            for entity_id, entity_name in existing_entities:
                if entity_name not in df[df_column].values:
                    cursor.execute(f"UPDATE Balancer{table_name} SET Active = 0, DateDisable = ? WHERE {table_name.lower()}_id = ? AND balancer_id = ?", (datetime.datetime.now(), entity_id, balancer_id))
                    # Lista para registrar historia
                    entities_to_disable.append(entity_id)

                    # Registrar el historial de cambios en la tabla de historial correspondiente
                    cursor.execute(f"INSERT INTO {table_name}StatusHistory ({id_column}, BalancerId, DateChange, IsActive, CommitMessage) VALUES (?, ?, ?, ?, ?)", (entity_id, balancer_id, datetime.datetime.now(), False, commit_message))

            # Identificar entidades que necesitan ser deshabilitadas en el DataFrame y actualizarlas si es necesario
            for entity_id in entities_to_disable:
                cursor.execute(f"SELECT COUNT(*) FROM Balancer{table_name} WHERE {table_name.lower()}_id = ? AND Active = 1", (entity_id,))
                active_count = cursor.fetchone()[0]

                if active_count == 0:
                    cursor.execute(f"UPDATE {table_name}s SET Active = 0, DateDisable = ? WHERE {id_column} = ?", (datetime.datetime.now(), entity_id))

        def update_status_history(entity_id, is_active, table_name):
            cursor.execute(f"INSERT INTO {table_name}StatusHistory ({table_name}Id, BalancerId, DateChange, isActive, CommitMessage) VALUES (?, ?, ?, ?, ?)",
                   (entity_id, balancer_id, datetime.datetime.now(), is_active, commit_message))

      
            """
            Insert or update values in the specified table.

            Args:
                cursor: The database cursor.
                table_name (str): The name of the table.
                columns (list): A list of column names.
                values (list): A list of values to be inserted or updated.
            
            # Check if the table has a primary key
            cursor.execute(f"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME = '{table_name}'")
            primary_key = cursor.fetchone()

            if primary_key:
                # If the table has a primary key, try to update the existing record
                set_values = ', '.join([f"{col} = ?" for col in columns])
                cursor.execute(f"UPDATE {table_name}StatusHistory SET {set_values} WHERE {primary_key[0]} = ?", values + [values[0]])
                # Check if the update affected any row
                if cursor.rowcount == 0:
                    # If no rows were updated, insert a new record
                    placeholders = ', '.join(['?' for _ in columns])
                    cursor.execute(f"INSERT INTO {table_name}StatusHistory ({', '.join(columns)}) VALUES ({placeholders})", values)
            else:
                # If the table doesn't have a primary key, just insert a new record
                placeholders = ', '.join(['?' for _ in columns])
                cursor.execute(f"INSERT INTO {table_name}StatusHistory ({', '.join(columns)}) VALUES ({placeholders})", values)"""

        def insert_entity(table_name, columns, values):
            """
            Insert values into the specified table.

            Args:
                cursor: The database cursor.
                table_name (str): The name of the table.
                columns (list): A list of column names.
                values (list): A list of values to be inserted.
            """
            column_names = ', '.join(columns)
            placeholders = ', '.join(['?' for _ in columns])
            #consulta=(f"INSERT INTO {table_name} ({column_names}) VALUES ({placeholders})", values)
            #if table_name=="Pools":
            #    print("CONSULTAS INSERT BALANCERNODEPOOL: ",consulta)
            cursor.execute(f"INSERT INTO {table_name} ({column_names}) VALUES ({placeholders})", values)

        def update_entity(table_name, columns, values, primary_key):
            """
                Update values in the specified table.

                Args:
                    cursor: The database cursor.
                    table_name (str): The name of the table.
                    columns (list): A list of column names.
                    values (list): A list of new values.
                    primary_key (str): The name of the primary key column.
            """
            set_values = ', '.join([f"{col} = ?" for col in columns])  
            consulta=(f"UPDATE {table_name} SET {set_values} WHERE {primary_key} = ?", values) 
            print("Esta es la consulta UPDATE: ", consulta)
            cursor.execute(f"UPDATE {table_name} SET {set_values} WHERE {primary_key} = ?", values)        

        ##########################################################################################################################

        # Array de Strings con cada uno de los objetos a parsear
        array = ["monitor", "node", "pool", "virtual", "irule"]

        # Open the configuration file
        with open(origin, 'r') as f:
            config = f.read()

        # Por cada objecto a parsear...
        for object in array:
            # Si el objeto es diferente de irule y de monitor...
            if (object != "irule" and object != "monitor"):
                # Patrón para localizar y capturar el nombre del objeto dentro del archivo
                pattern = re.compile(r'ltm '+object+' ([^ ]+)')
            # Si el objeto SÍ que es un irule...
            elif (object == "irule"):
                # Patrón para localizar y capturar el nombre del irule
                pattern = re.compile(r'ltm rule ([^ ]+)')
            # Si el objeto es un monitor...
            elif (object == 'monitor'):
                # Patrón para localizar y capturar el nombre del monitor dentro del archivo
                pattern = re.compile(r'ltm monitor ([^{]+)')

            # Array para guardar las coincidencias de cada objeto
            objects = pattern.findall(config)

            # Definción de lista para agregar diccionarios más adelante
            list = []
            i = 0
            # Por cada subobjeto dentro del mismo tipo de objetos...
            for subobject in objects:
                # Si el objeto es un nodo...
                if object == "node":
                    # Cuando la i declarada anteriormente vale 0 (significa que estamos tratrando con un tipo de objeto distinto)...
                    if i == 0:
                        # Patrón para localizar y capturar la IP de cada uno de los nodos
                        pattern2 = re.compile(
                            r'ltm node\s+/\w+\/[\w\d._-]+\s+{\s+address\s+([\d.]+)')
                        # Patrón para localizar y capturar la descripción de cada uno de los nodos
                        pattern3 = re.compile(
                            r'ltm node\s+/\w+\/[\w\d._-]+\s+{\s+address\s+[\d.]+(?:\s+description\s+((?:"[^"]+"|\S+)))?')
                        # Array para guardar todas las descripciones de cada uno de los nodos
                        descs = pattern3.findall(config)
                        # Array para guardar todas las IPs de cada uno de los nodos
                        IPs = pattern2.findall(config)
                    # Declaración de diccionario con campos de nodo
                    dict = {"Nodo": subobject,
                            "IP": IPs[i],
                            "Descripción": descs[i]}
                    # Agregar el diccionario a la lista declarada anteriormente
                    list.append(dict)
                    # Incrementar la i para que con el siguiente subobjeto coja sus respectivos valores (IP, descripción)
                    i = i+1
                # Si el objeto es una pool...
                elif object == "pool":
                    # Patrón para localizar y capturar los mimebros de la pool
                    pattern5 = re.compile(r'ltm pool ' + re.escape(subobject) + r' +{\s+(?:description\s+(?:"[^"]+"|\S+))?\s+(?:load-balancing-mode\s+[^\s]+)?\s+(?:members\s+{\s+(([^\s}]+)(?:[^}]+}\s*)*)*})?')

                    # Array para separar cada uno de los miembros en posiciones independientes
                    coincidencias = re.search(pattern5, config)
                    # Si existen coinicidencias...
                    if coincidencias:
                        # En caso de que las coincidencias estén vacías hay que capturar y controlar el error
                        try:
                            # Array para almacenar los miembros de cada pool
                            members = re.findall(
                                r'(/Common/[\w\d._-]+):', coincidencias.group(1))
                            # Array para almacenar los puertos de cada miembro de cada pool
                            puertos = re.findall(
                                r'/Common/[\w\d._-]+:([^\s]+)', coincidencias.group(1))
                            # Array para almacenar las direcciones IP de cada uno de los miembros de cada pool
                            direcciones = re.findall(
                                r'/Common/[\w\d._-]+:[^\s]+\s+{\s+(?:address\s+([^\s]+))?', coincidencias.group(1))
                            # Array para almacenar las descripciones de cada uno de los mimebros de cada pool
                            descripciones = re.findall(
                                r'/Common/[\w\d._-]+:[^\s]+\s+{\s+(?:address\s+[^\s]+)?\s+(?:description\s+((?:"[^"]+"|\S+)))?', coincidencias.group(1))
                        # Cuando no hay coincidencias salta una excepción...
                        except:
                            # Se ponen todos los arrays referentes a los miembros con un sólo String vacío para solucionar el error.
                            members = [""]
                            puertos = [""]
                            direcciones = [""]
                            descripciones = [""]
                    # Si la i es igual a 0 (significa que estamos tratrando con un tipo de objeto distinto)...
                    if i == 0:
                        # Patrón para localizar y capturar cada una de las descripciones de cada una de las pools
                        pattern2 = re.compile(
                            r'ltm pool\s+/\w+\/[\w\d._-]+\s+{\s+(?:description\s+((?:"[^"]+"|\S+)))?')
                        # Patrón para localizar y capturar cada uno de los balancing-mode de cada una de las pools
                        pattern3 = re.compile(
                            r'ltm pool\s+/\w+\/[\w\d._-]+\s+{\s+(?:description\s+(?:"[^"]+"|\S+))?\s+(?:load-balancing-mode\s+([^\s]+))?')
                        # Patrón para localizar y capturar cada uno de los monitores de cada una de las pools
                        pattern4 = re.compile(
                            r'ltm pool\s+/\w+\/[\w\d._-]+\s+{\s+(?:description\s+(?:"[^"]+"|\S+))?\s+(?:load-balancing-mode\s+[^\s]+)?\s+(?:members\s+{(?:[^}]+}\s*)*})?\s+(?:monitor\s+([^\s]+))?')
                        # Array para almacenar cada una de las descripciones de cada una de las pools
                        descs = pattern2.findall(config)
                        # Array para almacenar cada uno de los balancing-mode de cada una de las pools
                        mode = pattern3.findall(config)
                        # Array para almacenar cada uno de los monitores de cada una de las pools
                        monitor = pattern4.findall(config)
                    x = 0

                    # Por cada mimebro dentro del array de miembros...
                    for member in members:
                        # Creamos un diccionario con los siguientes campos
                        dict = {"Pool": subobject,
                                "Descripción (Pool)": descs[i],
                                "Tipo Balanceo (Pool)": mode[i],
                                "Nombre (Miembro):": member,
                                "Puerto (Miembro):": puertos[x],
                                "Dirección (Miembro):": direcciones[x],
                                "Descripción (Miembro):": descripciones[x],
                                "Monitor (Pool)": monitor[i]
                                }

                        # Se incrementa el valor de x para asignar correctamente el miembro correspondiente a cada diccionario
                        x = x+1
                        # Se añade el diccionario a la lista declarada anteriormente
                        list.append(dict)
                    # Se incremente el valor de i para asignar correctamente la pool correspondiente a cada diccionario
                    i = i+1

                elif object == 'monitor':
                    if i == 0:
                        pattern2 = re.compile(
                            r'ltm monitor [^{]+{(?:\s+adaptive\s+([^\s]+))?')
                        adaptive = pattern2.findall(config)
                        pattern6 = re.compile(
                            r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+([^\s]+))?')
                        cipherlist = pattern6.findall(config)
                        pattern7 = re.compile(
                            r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+([^\s]+))?')
                        compatibility = pattern7.findall(config)
                        pattern14 = re.compile(
                            r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+([^\s]+))?')
                        debug = pattern14.findall(config)
                        pattern3 = re.compile(
                            r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+([^\s]+))?')
                        defaults_from = pattern3.findall(config)
                        pattern4 = re.compile(
                            r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+((?:"[^"]+"|\S+)))?')
                        descs = pattern4.findall(config)
                        # http /Common/Casiopeams_ElectronicPrivatePrescription.Service_Priv
                        pattern5 = re.compile(
                            r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+([^\s]+))?')
                        dests = pattern5.findall(config)
                        pattern15 = re.compile(
                            r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+([^\s]+))?')
                        get = pattern15.findall(config)
                        pattern8 = re.compile(
                            r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+([^\s]+))?')
                        interval = pattern8.findall(config)
                        pattern16 = re.compile(
                            r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+([^\s]+))?')
                        password = pattern16.findall(config)
                        pattern17 = re.compile(
                            r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+([^\s]+))?')
                        server = pattern17.findall(config)
                        pattern18 = re.compile(
                            r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+([^\s]+))?')
                        service = pattern18.findall(config)
                        pattern8 = re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+([^\s]+))?')
                        ip_dscp = pattern8.findall(config)
                        pattern9 = re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+((?:"[^"]+"|\S+)))?')
                        recv = pattern9.findall(config)
                        pattern10 = re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+(?:"[^"]+"|\S+))?(?:\s+recv-disable\s([^\s]+))?')
                        recv_disable = pattern10.findall(config)
                        pattern20 = re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+(?:"[^"]+"|\S+))?(?:\s+recv-disable\s[^\s]+)?(?:\s+reverse\s+([^\s]+))?')
                        reverse = pattern20.findall(config)
                        pattern11 = re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+(?:"[^"]+"|\S+))?(?:\s+recv-disable\s[^\s]+)?(?:\s+reverse\s+[^\s]+)?(?:\s+send\s+((?:"[^"]+"|\S+)))?')
                        send = pattern11.findall(config)
                        pattern21 = re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+(?:"[^"]+"|\S+))?(?:\s+recv-disable\s[^\s]+)?(?:\s+reverse\s+[^\s]+)?(?:\s+send\s+(?:"[^"]+"|\S+))?(?:\s+ssl-profile\s+([^\s]+))?')
                        ssl_profile = pattern21.findall(config)
                        pattern12 = re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+(?:"[^"]+"|\S+))?(?:\s+recv-disable\s[^\s]+)?(?:\s+reverse\s+[^\s]+)?(?:\s+send\s+(?:"[^"]+"|\S+))?(?:\s+ssl-profile\s+[^\s]+)?(?:\s+time-until-up\s([^\s]+))?')
                        untilup = pattern12.findall(config)
                        pattern13 = re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+(?:"[^"]+"|\S+))?(?:\s+recv-disable\s[^\s]+)?(?:\s+reverse\s+[^\s]+)?(?:\s+send\s+(?:"[^"]+"|\S+))?(?:\s+ssl-profile\s+[^\s]+)?(?:\s+time-until-up\s[^\s]+)?(?:\s+timeout\s+([^\s]+))?')
                        timeout = pattern13.findall(config)
                        pattern19 = re.compile(r'ltm monitor [^{]+{(?:\s+adaptive\s+[^\s]+)?(?:\s+cipherlist\s+[^\s]+)?(?:\s+compatibility\s+[^\s]+)?(?:\s+debug\s+[^\s]+)?(?:\s+defaults-from\s+[^\s]+)?(?:\s+description\s+(?:"[^"]+"|\S+))?(?:\s+destination\s+[^\s]+)?(?:\s+get\s+[^\s]+)?(?:\s+interval\s+[^\s]+)?(?:\s+password\s+[^\s]+)?(?:\s+server\s+[^\s]+)?(?:\s+service\s+[^\s]+)?(?:\s+ip-dscp\s+[^\s]+)?(?:\s+recv\s+(?:"[^"]+"|\S+))?(?:\s+recv-disable\s[^\s]+)?(?:\s+reverse\s+[^\s]+)?(?:\s+send\s+(?:"[^"]+"|\S+))?(?:\s+ssl-profile\s+[^\s]+)?(?:\s+time-until-up\s[^\s]+)?(?:\s+timeout\s+[^\s]+)?(?:\s+username\s+([^\s]+))?')
                        username = pattern19.findall(config)
                    # Se agrega al diccionario simplemente el tipo de objeto y su nombre
                    dict = {object: subobject,
                            "Adaptive": adaptive[i],
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
                    # Se agrega el diccionario a la lista declarada anteriormente
                    list.append(dict)
                    i = i+1
                elif object == 'virtual':
                    if i == 0:
                        pattern2 = re.compile(
                            r'ltm virtual(?:.*?)(?=ltm virtual|$|pool\s)(?:pool\s+([^\s]+))?', re.DOTALL)
                        assignedPool = pattern2.findall(config)
                    pattern3 = re.compile(r'ltm virtual ' + re.escape(subobject) + r'(?:.*?)(?=ltm virtual|$|rules )(?:rules +(.*?)(?=}))?', re.DOTALL)

                    coincidencias = re.search(pattern3, config)
                    try:
                        rules = re.findall(
                            r'\s+([^\s]+)', coincidencias.group(1))
                    except:
                        rules = [""]
                    for rule in rules:
                        dict = {object: subobject,
                                "POOL": assignedPool[i],
                                "Rule": rule}
                        list.append(dict)
                    i = i+1
                # Si el objeto no es ni un nodo ni una pool...
                else:
                    print("Subobject: ",subobject)
                    pattern2 = re.compile(
                                r'ltm rule ' + re.escape(subobject) + r' (?:.*?)(?=ltm rule|if\s)(?:(.*?))?(?=}}|ltm rule|ltm)', re.DOTALL)
                    # (?:.*?)(?=ltm rule|$|if\s)(?:if\s*{\s*[(.*?)(?=}))?
                    coincidencias = re.search(pattern2, config)
                    if subobject == "/Common/https_request_casioserviceshiscl3.idc.local":
                        print(coincidencias)
                    #coinicidencias2 = pattern2.findall(config)
                    if coincidencias:
                        pools = re.findall(
                            r'if[^{]+[^}]+}\s+(?:(?:.*?)(?=pool|node|virtual)((?:pool|node|virtual)\s+[^\s]+))?', coincidencias.group(1), re.DOTALL)
                        condiciones = re.findall(
                            r'if[^{]+([^}]+})', coincidencias.group(1))
                        x = 0
                        for pool in pools:
                            dict = {"Rule": subobject,
                                    "Irule": condiciones[x],
                                    "Redireccionamiento": pool}
                            x = x+1
                            # Se agrega el diccionario a la lista declarada anteriormente
                            list.append(dict)
                    # pattern3=re.compile(r'ltm rule '+subobject+' (?:.*?)(?=pool\s)(?:pool\s+([^\s]+))?(?=ltm rule|}})', re.DOTALL)
                    # conditions2=pattern3.findall(config)
                    # Se agrega al diccionario simplemente el tipo de objeto y su nombre

            # Abrimos el json correspondiente de cada tipo de objeto en modo escritura (si no existe, se crea)...
            with open(object+'s.json', 'w') as f:
                # Se vuelca el contenido de la lista que se ha ido complementado todo este tiempo en formato json
                json.dump(list, f, indent=4)
            # Si todo va bien, nos avisa por consola que el archivo .json se ha creado correctamente
            print(object+"s.json creado correctamente")
            # Abrimos el .json recién creado...
            with open(object+'s.json') as data_file:
                # Obetenemos los objetos json del arhivo .json
                _datafile = json.load(data_file)
                # Convertimos cada una de las declaraciones en una linea de un formulario tabulado
                df_json = json_normalize(_datafile)
                # Pasamos las tabulaciones creadas en el paso anterior a un archivo .csv
                df_json.to_csv(object+"s.csv", index=False)

            # Si todo va bien, nos avisa por consola que se ha creado el archivo .csv correctamente.
            print(object+"s.csv creado correctamente")

        ###################################################################################################################

            if object == 'monitor':
                # Leer el archivo CSV de monitores
                df_monitors = pd.read_csv('monitors.csv')

                # Convertir valores NaN a None
                df_monitors = df_monitors.where(pd.notna(df_monitors), None)

                try:
                    # Iniciar una transacción
                    #cursor.execute("BEGIN TRANSACTION")

                    disable_relationships(df_monitors, 'monitor', 'Monitors', 'MonitorId')
       
                    # Lista para mantener los monitores que necesitan ser habilitados en BalancerMonitor
                    monitors_to_enable = []

                    monitor_columns = ['Active', 'Adaptive', 'Cipherlist', 'Compatibility', 'DateInsert', 'Debug', 'Defaults_from', 'Description', 'Destination', 'IP_DSCP', 'Interval', 'Name', 'Password', 'RECV', 'RECV_disable', 'Reverse', 'SEND', 'Server', 'Service', 'get', 'ssl_profile', 'time_until_up', 'timeout', 'username']
                    bm_columns = ['DateInsert', 'DateModify', 'DateDisable', 'Active', 'monitor_id', 'balancer_id']

                    # Ahora, iterar sobre los monitores del archivo introducido
                    for row in df_monitors.itertuples():
                        monitor_name = row.monitor

                        # Verificar si el monitor ya existe en la base de datos
                        cursor.execute("SELECT MonitorId, Active FROM Monitors WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?", (monitor_name,))
                        existing_monitor = cursor.fetchone()

                        if existing_monitor is None:
                            # El monitor no existe, insertarlo en la base de datos
                            values = [True, row.Adaptive, row.Cipherlist, row.Compatibility, datetime.datetime.now(), row.Debug, row.Defaults_from, row.Description, row.Destination, row.IP_DSCP, row.Interval, monitor_name, row.password, row.RECV, row.RECV_DISABLE, row.reverse, row.SEND, row.server, row.service, row.get, row.ssl_profile, row.time_until_up, row.timeout, row.username]
                            insert_entity("Monitors", monitor_columns, values)
                            

                            # Obtener el ID del monitor insertado
                            #cursor.execute("SELECT SCOPE_IDENTITY()")
                            cursor.execute("SELECT MonitorId FROM Monitors WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?", monitor_name)
                            monitor_id = cursor.fetchone()[0]

                            # Insertar en BalancerMonitor                            
                            values = [datetime.datetime.now(), datetime.datetime.now(), None, True, monitor_id, balancer_id]
                            insert_entity("BalancerMonitor", bm_columns, values)

                            # Agregar a la lista de monitores que han sido habilitados
                            monitors_to_enable.append(monitor_id)

                        else:
                            # El monitor ya existe en la base de datos
                            existing_monitor_id, active_monitor = existing_monitor                            

                            # Verificar si el monitor está inactivo en Monitors
                            if active_monitor is not None and active_monitor == 0:
                                values = [True, None, row.Adaptive, row.Cipherlist, row.Compatibility, datetime.datetime.now(), row.Debug, row.Defaults_from, row.Description, row.Destination, row.IP_DSCP, row.Interval, monitor_name, row.password, row.RECV, row.RECV_DISABLE, row.reverse, row.SEND, row.server, row.service, row.get, row.ssl_profile, row.time_until_up, row.timeout, row.username, existing_monitor_id]
                                primary_key = 'MonitorId'
                                update_entity("Monitors", ['Active', 'DateDisable', 'Adaptive', 'Cipherlist', 'Compatibility', 'DateModify', 'Debug', 'Defaults_from', 'Description', 'Destination', 'IP_DSCP', 'Interval', 'Name', 'Password', 'RECV', 'RECV_disable', 'Reverse', 'SEND', 'Server', 'Service', 'get', 'ssl_profile', 'time_until_up', 'timeout', 'username'], values, primary_key)

                            # Verificar si el monitor está inactivo en BalancerMonitor                                
                            cursor.execute("SELECT Active FROM BalancerMonitor WHERE monitor_id = ? AND balancer_id = ?", (existing_monitor_id, balancer_id,))
                            monitor_active = cursor.fetchone()

                            # Verificar si el monitor está inactivo en BalancerMonitor o no existe
                            if monitor_active[0] is None:
                                # Insertar el monitor en BalancerMonitor
                                values = [datetime.datetime.now(), datetime.datetime.now(), None, True, monitor_id, balancer_id]
                                insert_entity("BalancerMonitor", bm_columns, values)      

                            elif monitor_active[0] == 0:
                                # El monitor está inactivo en BalancerMonitor, activarlo
                                cursor.execute("UPDATE BalancerMonitor SET Active = 1, DateDisable = NULL, DateMoidify = ? WHERE monitor_id = ? AND balancer_id = ?", (datetime.datetime.now(), existing_monitor_id, balancer_id,))                                

                            # Agregar a la lista de monitores que han sido habilitados
                            monitors_to_enable.append(existing_monitor_id)                               
       
                    # Volver a habilitar los monitores que han reaparecido
                    for monitor_id in monitors_to_enable:
                        update_status_history(monitor_id, True, "Monitor" )

                    # Confirmar la transacción
                    #cursor.execute("COMMIT")

                except Exception as e:
                    # Revertir la transacción en caso de error
                    #cursor.execute("ROLLBACK")
                    print(Exception)

            elif object == 'pool':
                df_pools = pd.read_csv('pools.csv')

                df_pools = df_pools.where(pd.notna(df_pools), None)

                df_pools = pd.DataFrame(df_pools)

                bp_columns = ['DateInsert', 'DateModify', 'DateDisable', 'Active', 'pool_id', 'balancer_id']
                pool_columns = ['MonitorId', 'Active', 'BalancerType', 'DateInsert', 'Description', 'Name']
                node_columns = ['Active', 'DateInsert', 'Description', 'IP', 'Name']
                bn_columns = ['DateInsert', 'DateModify', 'DateDisable', 'Active', 'node_id', 'balancer_id']
                bnp_columns = ['DateInsert', 'DateModify', 'DateDisable', 'Active', 'node_id', 'pool_id', 'balancer_id']
                np_columns = ['NodeId', 'PoolId', 'Active', 'DateInsert', 'Name', 'NodePort']
  
                disable_relationships(df_pools, 'Pool', 'Pools', 'PoolId')

                # Lista para mantener los pools que necesitan ser habilitados en BalancerPool
                pools_to_enable = []

                # Lista para mantener los nodepool que necesitan ser deshabilitados en BalancerNodePool
                nodepool_to_disable = []
                # Lista para mantener los nodepool que necesitan ser habilitados en BalancerNodePool
                nodepool_to_enable = []

                # Iterar a través de los registros existentes en NodePool
                cursor.execute("SELECT NodeId, PoolId FROM NodePool")
                existing_nodepools = cursor.fetchall()

                # Asegurarse de que el DataFrame df está definido antes de la iteración
                df_columns = df_pools.columns

                # Crear un conjunto de nodos y pools presentes en el DataFrame
                df_nodes_pools = set((row[df_columns.get_loc('Nombre (Miembro):') + 1], row.Pool) for row in df_pools.itertuples())
  
                # Iterar a través de los registros existentes en BalancerNodePool
                for node_id, pool_id in existing_nodepools:
                    # Verificar si el registro no está presente en el DataFrame
                    if (node_id, pool_id) not in df_nodes_pools:
                        # Deshabilitar la relación en BalancerNodePool
                        cursor.execute("UPDATE BalancerNodePool SET Active = 0, DateDisable = ? WHERE node_id = ? AND pool_id = ? AND balancer_id = ?", 
                                    (datetime.datetime.now(), node_id, pool_id, balancer_id))
                        # Registrar en la lista de nodepools a deshabilitar
                        nodepool_to_disable.append((node_id, pool_id))
                
                # Iterar a través de los registros existentes en NodePool
                for node_id, pool_id in existing_nodepools:
                    # Verificar si todos los registros están deshabilitados en BalancerNodePool
                    cursor.execute("SELECT COUNT(*) FROM BalancerNodePool WHERE node_id = ? AND pool_id = ? AND Active = 1", 
                                (node_id, pool_id))
                    active_count = cursor.fetchone()[0]

                    if active_count == 0:
                        # Deshabilitar el registro en NodePool
                        cursor.execute("UPDATE NodePool SET Active = 0, DateDisable = ? WHERE NodeId = ? AND PoolId = ?", 
                                    (datetime.datetime.now(), node_id, pool_id))

                # Iterar a través de las filas del DataFrame
                for row in df_pools.itertuples():
                    monitor_pool_index = df_pools.columns.get_loc('Monitor (Pool)')
                    monitor_pool = row[monitor_pool_index + 1]  # Acceder por índice

                    # Realizar una consulta SQL para buscar el MonitorId en la tabla Monitors
                    query = "SELECT MonitorId FROM Monitors WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE ?"
                    cursor.execute(query, f'%{monitor_pool} ')

                    # Recuperar el MonitorId desde la consulta
                    monitor_id = cursor.fetchone()

                    # Asignar el MonitorId al DataFrame
                    if monitor_id:
                        df_pools.at[row.Index, 'MonitorId'] = monitor_id[0]
                    else:
                        df_pools.at[row.Index, 'MonitorId'] = None

                    if monitor_id is None:
                        print(f"No se encontró ningún monitor para el pool: '{monitor_pool}'")

                # Reemplazar "nan" por None utilizando pd.notna
                df_pools['MonitorId'] = df_pools['MonitorId'].where(pd.notna(df_pools['MonitorId']), None)

                for row in df_pools.itertuples():
                    balancer_type_index = df_pools.columns.get_loc('Tipo Balanceo (Pool)')
                    description_index = df_pools.columns.get_loc('Descripción (Pool)')
                    pool_name = row.Pool

                    # Verificar si ya existe un registro con el mismo valor en la columna 'Name'
                    cursor.execute("""
                        SELECT COUNT(*)
                        FROM Pools
                        WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?
                    """, (pool_name,))
                    result = cursor.fetchone()

                    if result[0] is not None and result[0] > 0:
                        # El registro ya existe
                        # Verificar si está inactivo y actualizarlo con los nuevos datos
                        cursor.execute("""
                                SELECT PoolId, Active
                                FROM Pools
                                WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?
                        """, (pool_name,))
                        fetched_pool = cursor.fetchone()

                        if fetched_pool[1] is not None and fetched_pool[1]==0:
                            # Actualizar el registro existente y activarlo
                            values = [df_pools.at[row.Index, 'MonitorId'], True, row[balancer_type_index + 1], datetime.datetime.now(), row[description_index + 1], None, fetched_pool[0]]
                            update_entity("Pools", ['MonitorId', 'Active', 'BalancerType', 'DateModify', 'Description', 'DateDisable'], values, "PoolId")

                            # Verificar si existe en BalancerPool y activarlo en caso de estar desactivado
                            cursor.execute("""
                                SELECT Active
                                FROM BalancerPool
                                WHERE pool_id = ? AND balancer_id = ?
                            """, (fetched_pool[0], balancer_id))
                            bp_result = cursor.fetchone()

                            if bp_result[0] is not None and bp_result[0]==0:
                                # El registro existe en BalancerPool y está desactivado
                                values = [datetime.datetime.now(),None, True, fetched_pool[0]]
                                primary_key = 'PoolId'
                                update_entity("BalancerPool", ['DateModify', 'DateDisable', 'Active', 'pool_id'], values, primary_key)
                                
                                print(f"Registro Reactivado para pool: {pool_name} En la relación con: {balancer_name}")

                            elif bp_result is None:
                                # El registro no existe en BalancerPool, insertarlo
                                values = [datetime.datetime.now(), datetime.datetime.now(), None, True, fetched_pool[0], balancer_id]
                                insert_entity("BalancerPool", bp_columns, values)
                            
                            # Agregar a la lista de monitores que han sido habilitados
                            pools_to_enable.append(fetched_pool[0])

                    else:
                        # El pool no existe en la tabla Pools
                        # Insertar un nuevo registro en la tabla Pools
                        values = [df_pools.at[row.Index, 'MonitorId'], True, row[balancer_type_index + 1] if not pd.isna(row[balancer_type_index + 1]) else "", datetime.datetime.now(), row[description_index + 1] if not pd.isna(row[description_index + 1]) else "", pool_name]
                        insert_entity("Pools", pool_columns, values)

                        # Obtener el PoolId del nuevo registro insertado
                        #cursor.execute("SELECT SCOPE_IDENTITY()")
                        cursor.execute("SELECT PoolId FROM Pools WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?", pool_name)
                        new_pool_id = cursor.fetchone()[0]

                        # Insertar el nuevo registro en BalancerPool
                        values = [datetime.datetime.now(), datetime.datetime.now(), None, True, new_pool_id, balancer_id]
                        insert_entity("BalancerPool", bp_columns, values)

                        # Agregar a la lista de monitores que han sido habilitados
                        pools_to_enable.append(new_pool_id)
                    
                    name_member = df_pools.columns.get_loc('Nombre (Miembro):')

                    if row[name_member + 1] != '':
                        member_name = row[name_member + 1]  # Nombre (Miembro)

                        # Verificar si ya existe un registro con el mismo valor en la columna 'Name' en Nodes
                        cursor.execute("SELECT COUNT(*) FROM Nodes WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?", (member_name))
                        result2 = cursor.fetchone()


                        if result2 is not None and result2[0] > 0:
                            print(f"Registro existente para Miembro: {member_name} De Pool: {pool_name} No se inserta pero se asigna a pool si no lo estaba.") 
                            cursor.execute("SELECT NodeId FROM Nodes WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?", (member_name,)) 
                            node_exists=cursor.fetchone()

                            cursor.execute("SELECT PoolId FROM Pools WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?", (pool_name,))
                            pool_exists=cursor.fetchone()
                                
                            if node_exists[0] is not None:
                                cursor.execute("SELECT Active FROM NodePool WHERE NodeId = ? AND PoolId = ?",(node_exists[0], pool_exists[0]))
                                node_pool_active=cursor.fetchone()

                                if node_pool_active is None:
                                    node_pool_name = f"{member_name}----{pool_name}"
                                    port_member = df_pools.columns.get_loc('Puerto (Miembro):')                                        
                                    values = [node_exists[0], pool_exists[0], True, datetime.datetime.now(), node_pool_name, row[port_member + 1]]
                                    insert_entity("NodePool", np_columns, values)

                                    values = [datetime.datetime.now(), datetime.datetime.now(), None, True, node_exists[0], pool_exists[0], balancer_id]
                                    insert_entity("BalancerNodePool", bnp_columns, values)

                                    nodepool_to_enable.append((node_exists[0],  pool_exists[0]))
                                elif node_pool_active[0]==0:
                                    cursor.execute("UPDATE NodePool SET Active=1, DateDisable = ?, DateModify = ? WHERE NodeId = ? AND PoolId = ?",(None, datetime.datetime.now(), node_exists[0], pool_exists[0])) 

                                    cursor.execute("""
                                        SELECT Active
                                        FROM BalancerNodePool
                                        WHERE pool_id = ? AND node_id = ? AND balancer_id = ?
                                    """, (pool_exists[0], node_exists[0], balancer_id))
                                    bnp_result = cursor.fetchone()   

                                    if bnp_result[0] is None:
                                        values = [datetime.datetime.now(), datetime.datetime.now(), None, True, node_exists[0], pool_exists[0], balancer_id]
                                        insert_entity("BalancerNodePool", bnp_columns, values)
                                    elif bnp_result[0]==0:
                                        cursor.execute('''
                                            UPDATE BalancerNodePool
                                            SET Active = ?, DateDisable = ?, DateModify = ?
                                            WHERE pool_id = ? AND node_id = ? AND balancer_id = ?
                                        ''', (True,None, datetime.datetime.now(), pool_exists[0], node_exists[0], balancer_id))
                                    nodepool_to_enable.append((node_exists[0],  pool_exists[0]))
                        else:
                            description_node = df_pools.columns.get_loc('Descripción (Miembro):')
                            direction_member = df_pools.columns.get_loc('Dirección (Miembro):')

                            # Insertar en la tabla Nodes
                            values = [True, datetime.datetime.now(), row[description_node + 1], row[direction_member + 1], member_name]
                            insert_entity("Nodes", node_columns, values)

                            # Obtener el ID del node insertado
                            #cursor.execute("SELECT SCOPE_IDENTITY()")
                            cursor.execute("SELECT NodeId FROM Nodes WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?", member_name)
                            member_id = cursor.fetchone()[0]

                            values = [datetime.datetime.now(), datetime.datetime.now(), None, True, member_id, balancer_id]
                            insert_entity("BalancerNode", bn_columns, values)

                        # Verificar si ya existe un registro con el mismo valor en la tabla NodePool
                        node_pool_name = f"{member_name}----{pool_name}"
                        cursor.execute("""
                            SELECT Active
                            FROM NodePool
                            WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?
                        """, (node_pool_name,))
                        result3 = cursor.fetchone()

                        cursor.execute("SELECT PoolId FROM Pools WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?", (pool_name,))
                        fetched_pool=cursor.fetchone()

                        cursor.execute("SELECT NodeId FROM Nodes WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?", member_name)
                        member_id = cursor.fetchone()[0]

                        if result3[0] is not None:
                            print(f"Registro duplicado para NodePool: {node_pool_name}")

                            if result3[0]==0:
                                cursor.execute("UPDATE NodePool SET Active=1, DateDisable = ?, DateModify = ? WHERE NodeId = ? AND PoolId = ?",(None, datetime.datetime.now(), member_id, fetched_pool[0])) 

                            # Verificar si existe en BalancerNode y activarlo en caso de estar desactivado
                            cursor.execute("""
                                SELECT Active
                                FROM BalancerNodePool
                                WHERE pool_id = ? AND node_id = ? AND balancer_id = ?
                            """, (fetched_pool[0], member_id, balancer_id))
                            bn_result = cursor.fetchone()

                            if bn_result[0] is not None and bn_result[0]==0:
                                # El registro existe en BalancerPool
                                cursor.execute('''
                                    UPDATE BalancerNodePool
                                    SET Active = ?, DateDisable = ?, DateModify = ?,
                                    WHERE pool_id = ? AND node_id = ? AND balancer_id = ?
                                ''', (True, None, datetime.datetime.now(), fetched_pool[0], member_id, balancer_id))

                            elif bn_result is None:
                                # El registro no existe en BalancerPool, insertarlo
                                values = [datetime.datetime.now(), datetime.datetime.now(), None, True, member_id, fetched_pool[0], balancer_id]
                                insert_entity("BalancerNodePool", bnp_columns, values)
                            
                            nodepool_to_enable.append((member_id, fetched_pool[0]))  
                                                                    
                        else:
                            port_member = df_pools.columns.get_loc('Puerto (Miembro):')

                            # Insertar en la tabla NodePool
                            values = [member_id, fetched_pool[0], True, datetime.datetime.now(), node_pool_name, row[port_member + 1]]
                            insert_entity("NodePool", np_columns, values)

                            # Insertar en la tabla BalancerNodePool
                            values = [datetime.datetime.now(), datetime.datetime.now(), None, True, member_id, fetched_pool[0], balancer_id]
                            insert_entity("BalancerNodePool", bnp_columns, values)

                            nodepool_to_enable.append((member_id, fetched_pool[0])) 

               # Volver a habilitar los pools que han reaparecido
                for pool_id in pools_to_enable:
                    update_status_history(pool_id, True, "Pool")
                                    
                # Deshabilitar los nodepools en la tabla NodePool
                for node_id, pool_id in nodepool_to_disable:
                    cursor.execute("INSERT INTO NodePoolStatusHistory (NodeId, PoolId, BalancerId, DateChange, IsActive, CommitMessage) VALUES (?, ?, ?, ?, ?, ?)", (node_id, pool_id, balancer_id, datetime.datetime.now(), False, commit_message))

                # Volver a habilitar los nodepools que han reaparecido
                for node_id, pool_id in nodepool_to_enable:
                    cursor.execute("INSERT INTO NodePoolStatusHistory (NodeId, PoolId, BalancerId, DateChange, IsActive, CommitMessage) VALUES (?, ?, ?, ?, ?, ?)", (node_id, pool_id, balancer_id, datetime.datetime.now(), True, commit_message))

          
            elif object == 'virtual':
                df_virtuals = pd.read_csv('virtuals.csv')

                df_virtuals = df_virtuals.where(pd.notna(df_virtuals), None)

                df_virtuals = pd.DataFrame(df_virtuals)

                virtual_columns = ['Active', 'DateInsert', 'Name', 'PoolId']
                bv_columns = ['DateInsert', 'DateModify', 'DateDisable', 'Active', 'virtual_id', 'balancer_id']
                br_columns = ['DateInsert', 'DateModify', 'DateDisable', 'Active', 'rule_id', 'balancer_id']
                rule_columns = ['Active', 'DateInsert', 'Name', 'VirtualId']

                disable_relationships(df_virtuals, 'virtual', 'Virtuals', 'VirtualId')
            
                # Lista para mantener los virtuals que necesitan ser habilitados en BalancerVirtual
                virtuals_to_enable = []

                for row in df_virtuals.itertuples():
                    cursor.execute(
                        "SELECT COUNT(*) FROM Virtuals WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =?", (row.virtual,))
                    result4 = cursor.fetchone()

                    cursor.execute(
                            "SELECT PoolId FROM Pools WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =?", row.POOL)
                    poolId = cursor.fetchone()

                    # Asignar el valor de PoolId a None si poolId es nulo
                    PoolId = poolId[0] if poolId is not None else None

                    if result4[0] is not None and result4[0] > 0:
                        print(f"Registro duplicado para: {row.virtual}")
                        cursor.execute(
                            "SELECT Active, VirtualId FROM Virtuals WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =?", (row.virtual,))
                        virtual_active = cursor.fetchone()

                        if virtual_active[0] is not None and virtual_active[0] == 0:
                            values = [1, datetime.datetime.now(), None, PoolId, row.Virtual]
                            primary_key = 'Name'  # Reemplaza 'Name' con el nombre de tu clave primaria en la tabla 'Virtuals'
                            update_entity("Virtuals",  ['Active', 'DateModify', 'DateDisable' 'PoolId'] , values, primary_key)
 
                        cursor.execute("SELECT Active FROM BalancerVirtual WHERE virtual_id =? AND balancer_id =?", (virtual_active[1], balancer_id))
                        bv_active = cursor.fetchone()

                        if bv_active is not None and bv_active[0] == 0:
                            cursor.execute("UPDATE BalancerVirtual SET Active=1, DateModify = ?, DateDisable = ?, WHERE virtual_id =? AND balancer_id =?", (virtual_active[1], datetime.datetime.now(), None, balancer_id))
                        elif bv_active is None:
                            values = [datetime.datetime.now(), datetime.datetime.now(), None, True, virtual_active[1], balancer_id]
                            insert_entity("BalancerVirtual", bv_columns, values)
                        virtuals_to_enable.append(virtual_active[1])
                            
                    else:                    
                        values = [True, datetime.datetime.now(), row.virtual, PoolId]
                        insert_entity("Virtuals", virtual_columns, values)
                        
                        # Obtener el ID del registro insertado
                        #cursor.execute("SELECT SCOPE_IDENTITY()")
                        cursor.execute("SELECT VirtualId FROM Virtuals WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?", row.virtual)
                        virtual_id = cursor.fetchone()[0]
                        
                        values = [datetime.datetime.now(), datetime.datetime.now(), None, True, virtual_id, balancer_id]
                        insert_entity("BalancerVirtual", bv_columns, values)
                        
                        virtuals_to_enable.append(virtual_id)

                        cursor.execute(
                            "SELECT COUNT(*) FROM Rules WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =?", row.Rule)
                        result5 = cursor.fetchone()

                        if (result5[0] is not None and result5[0] > 0) and row.Rule is not None:
                            print(f"Registro duplicado para: {row.Rule}")

                            cursor.execute("SELECT Active, RuleId FROM Rules WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =?",(row.Rule,))
                            rule_active=cursor.fetchone()

                            if rule_active[0] is not None and rule_active[0]==0:
                                cursor.execute("UPDATE Rules SET Active=1, DateModify =?, DateDisable=?, WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =?",(datetime.datetime.now(), None, row.Rule,))
                            
                            cursor.execute("SELECT Active FROM BalancerRule WHERE balancer_id =? AND rule_id=?",(balancer_id, rule_active[1]))
                            br_active=cursor.fetchone()

                            if br_active[0] is not None and br_active[0] == 0:
                                cursor.execute("UPDATE BalancerRule SET Active=1, DateModify =?, DateDisable =?, WHERE balancer_id =? AND rule_id=?",(datetime.datetime.now(), None, balancer_id, rule_active[1]))
                            elif not br_active:
                                values = [datetime.datetime.now(), datetime.datetime.now(), None, True, rule_active[1], balancer_id]
                                insert_entity("BalancerRule", br_columns, values)
                        elif row.Rule is not None:
                            cursor.execute(
                                "SELECT VirtualId FROM Virtuals WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =?", row.virtual)
                            virtualId = cursor.fetchone()

                            values = [True, datetime.datetime.now(), row.Rule, virtualId[0]]
                            insert_entity("Rules", rule_columns, values)
                            
                            # Obtener el ID del registro insertado
                            #cursor.execute("SELECT SCOPE_IDENTITY()")
                            cursor.execute("SELECT RuleId FROM Rules WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?", row.Rule)
                            rule_id = cursor.fetchone()[0]
                            
                            values = [datetime.datetime.now(), datetime.datetime.now(), None, True, rule_id, balancer_id]
                            insert_entity("BalancerRule", br_columns, values)

                # Volver a habilitar los virtuales que han reaparecido
                for virtual_id in virtuals_to_enable:
                    update_status_history(virtual_id, True, "Virtual")

            elif (object == 'irule'):
                df_rules = pd.read_csv('irules.csv')

                df_rules = df_rules.where(pd.notna(df_rules), None)

                df_rules = pd.DataFrame(df_rules)

                br_columns = ['DateInsert', 'DateModify', 'DateDisable', 'Active', 'rule_id', 'balancer_Id']
                rule_columns = ['Active', 'DateInsert', 'Name', 'VirtualId']
                bi_columns = ['DateInsert', 'DateModify', 'DateDisable', 'Active', 'irule_id', 'balancer_id']
                irule_columns = ['Active', 'DateInsert', 'Name', 'Redirect', 'RuleId']
                
                disable_relationships(df_rules, 'Rule', 'Rules', 'RuleId')

                disable_relationships(df_rules, 'Irule', 'Irules', 'IruleId')

                # Lista para mantener los rules que necesitan ser habilitados en BalancerRule
                rules_to_enable = []
                # Lista para mantener los irules que necesitan ser habilitados en BalancerIrule
                irules_to_enable = []

                # Obtener todos los rules existentes en la base de datos
                cursor.execute("SELECT RuleId, Name FROM Rules")
                existing_rules = cursor.fetchall()

                # Seleccionar la columna 'Rule' y eliminar duplicados
                distinct_rules = df_rules['Rule'].drop_duplicates()

                # Iterar sobre las filas únicas
                for rule in distinct_rules:
                    cursor.execute("SELECT COUNT(*) FROM Rules WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =?", (rule,))
                    result7 = cursor.fetchone()

                    if (result7[0] is not None and result7[0] > 0) and rule != '':
                        print(f"Registro duplicado para: {rule}")

                        cursor.execute("SELECT Active, RuleId FROM Rules WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =?", (rule,))
                        rule_active = cursor.fetchone()

                        if rule_active[0] is not None and rule_active[0] == 0:
                            cursor.execute("UPDATE Rules SET Active=1, DateModify =?, DateDisable=? WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =?", (datetime.datetime.now(), None, rule,))

                        cursor.execute("SELECT Active FROM BalancerRule WHERE balancer_id=? AND rule_id=?", (balancer_id, rule_active[1]))
                        br_active = cursor.fetchone()

                        if br_active[0] is not None and br_active[0] == 0:
                            cursor.execute("UPDATE BalancerRule SET Active=1, DateDisable=?, DateModify=? WHERE balancer_id=? AND rule_id=?",(None, datetime.datetime.now(), balancer_id, rule_active[1]))

                        elif br_active is None:
                            values = [datetime.datetime.now(), datetime.datetime.now(), None, True, rule_active[1], balancer_id]
                            insert_entity("BalancerRule", br_columns, values)

                        rules_to_enable.append(rule_active[1])
                    elif rule != '':
                        values = [True, datetime.datetime.now(), rule, None]
                        insert_entity("Rules", rule_columns, values)

                        # Obtener el ID del registro insertado
                        cursor.execute("SELECT RuleId FROM Rules WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?", (rule,))
                        rule_id = cursor.fetchone()[0]

                        values = [datetime.datetime.now(), datetime.datetime.now(), None, True, rule_id, balancer_id]
                        insert_entity("BalancerRule", br_columns, values)

                        rules_to_enable.append(rule_id)
               
                for row in df_rules.itertuples():
                    cursor.execute(
                        "SELECT RuleId FROM Rules WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =?", row.Rule)
                    ruleId = cursor.fetchone()

                    cursor.execute(
                        "SELECT COUNT(*) FROM Irules WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =? AND RuleId =?", (row.Irule, ruleId[0]))
                    result6 = cursor.fetchone()

                    if result6[0] is not None and result6[0] > 0:
                        print(f"Registro duplicado para: {row.Irule}")

                        cursor.execute("SELECT Active, IruleId FROM Irules WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =? AND RuleId=?",(row.Irule, ruleId[0]))
                        irule_active=cursor.fetchone()

                        if irule_active[0] is not None and irule_active[0]==0:
                            cursor.execute("UPDATE Irules SET Active=1, DateDisable=?, DateModify=? WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =? AND RuleId=?",(None, datetime.datetime.now(), row.Irule, ruleId[0]))
                            
                        cursor.execute("SELECT Active FROM BalancerIrule WHERE balancer_id=? AND irule_id=?",(balancer_id, irule_active[1]))
                        bi_active=cursor.fetchone()

                        if bi_active[0] is not None and bi_active[0] == 0:
                            cursor.execute("UPDATE BalancerIrule SET Active=1, DateDisable=?, DateModify=? WHERE balancer_id=? AND irule_id=?",(None, datetime.datetime.now(), balancer_id, irule_active[1]))
                            
                        elif bi_active is None:
                            values = [datetime.datetime.now(), datetime.datetime.now(), None, True, irule_active[1], balancer_id]
                            insert_entity("BalancerIrule", bi_columns, values)
                        irules_to_enable.append(irule_active[1])
                    else:
                        values = [True, datetime.datetime.now(), row.Irule, row.Redireccionamiento, ruleId[0]]
                        insert_entity("Irules", irule_columns, values)
                        
                        # Obtener el ID del registro insertado
                        #cursor.execute("SELECT SCOPE_IDENTITY()")
                        cursor.execute("SELECT IruleId FROM Irules WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ? AND RuleId=?", (row.Irule, ruleId[0]))
                        irule_id = cursor.fetchone()[0]
                            
                        values = [datetime.datetime.now(), datetime.datetime.now(), None, True, irule_id, balancer_id]
                        insert_entity("BalancerIrule", bi_columns, values)

                        irules_to_enable.append(irule_id)

                # Volver a habilitar las reglas que han reaparecido
                for rule_id in rules_to_enable:
                   update_status_history(rule_id, True, "Rule")

                # Volver a habilitar las ireglas que han reaparecido
                for irule_id in irules_to_enable:
                    update_status_history(irule_id, True, "Irule")
                    
            elif object == 'node':
                df_nodes = pd.read_csv('nodes.csv')

                df_nodes = df_nodes.where(pd.notna(df_nodes), None)

                df_nodes = pd.DataFrame(df_nodes)

                node_columns = ['Active', 'DateInsert', 'Description', 'IP', 'Name']
                bn_columns = ['DateInsert', 'DateModify', 'DateDisable', 'Active', 'node_id', 'balancer_id']

                # Lista para mantener los virtuals que necesitan ser habilitados en BalancerNode
                nodes_to_enable = []

                # Llamada a la función para deshabilitar las relaciones de nodos
                disable_relationships(df_nodes, 'Nodo', 'Nodes', 'NodeId')

                for row in df_nodes.itertuples():
                    cursor.execute(
                        "SELECT COUNT(*) FROM Nodes WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =?", row.Nodo)
                    result7 = cursor.fetchone()

                    if result7[0] is not None and result7[0] > 0:
                        print(f"Registro duplicado para: {row.Nodo}")

                        cursor.execute("SELECT Active, NodeId FROM Nodes WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS =?",(row.Nodo,))
                        node_active=cursor.fetchone()

                        if node_active[0] is not None and node_active[0]==0:
                            values = [True, datetime.datetime.now(), None, row.Descripción if not pd.isna(row.Descripción) else "", row.IP, row.Nodo]
                            update_entity("Nodes", ['Active', 'DateModify', 'DateDisable', 'Description', 'IP'] , values, "Name")
                            
                        cursor.execute("SELECT Active FROM BalancerNode WHERE node_id=?",(node_active[1],))
                        bn_active=cursor.fetchone()

                        if bn_active[0] is not None and bn_active[0]==0:
                            cursor.execute("UPDATE BalancerNode SET Active=1, DateModify=?, DateDisable=? WHERE node_id=?",(datetime.datetime.now(), None, node_active[1],))
                        elif bn_active is None:
                            values = [datetime.datetime.now(), datetime.datetime.now(), None, True, node_active[1], balancer_id]
                            insert_entity("BalancerNode", bn_columns, values)
                        nodes_to_enable.append(node_active[1])

                    else:
                        # Convertir valores nan a None
                        values = [True, datetime.datetime.now(), row.Descripción if not pd.isna(row.Descripción) else "", row.IP, row.Nodo]
                        # Llamar a insert_entity con los valores actualizados
                        insert_entity("Nodes", node_columns, values)

                        # Obtener el ID del registro insertado
                        #cursor.execute("SELECT SCOPE_IDENTITY()")
                        cursor.execute("SELECT NodeId FROM Nodes WHERE Name COLLATE SQL_Latin1_General_CP1_CS_AS = ?", row.Nodo)
                        node_id = cursor.fetchone()[0]

                        # Insertar en BalancerNode
                        values = [datetime.datetime.now(), datetime.datetime.now(), None, 1, node_id, balancer_id]
                        insert_entity("BalancerNode", bn_columns, values)

                        
                        nodes_to_enable.append(node_id)

                 # Volver a habilitar las reglas que han reaparecido
                
                for node_id in nodes_to_enable:
                   update_status_history(node_id, True, "Node")      

        ###################################################################################################################

            # Eliminar jsons para limpiar (comentar en caso de querer mantener los .json)
            file = object+'s.json'
            if (os.path.exists(file) and os.path.isfile(file)):
                os.remove(file)
                print(object+('s.json deleted'))

        # Lista de nombres de archivos CSV a combinar
        archivos_csv = []
        #["nodes.csv", "pools.csv", "irules.csv", "virtuals.csv", "monitors.csv"]

        # Crear un DataFrame para cada archivo CSV y almacenarlos en un diccionario
        dataframes = {}
        for archivo in archivos_csv:
            # Usaremos el nombre del archivo como nombre de la hoja
            nombre_hoja = archivo.split(".")[0]
            dataframes[nombre_hoja] = pd.read_csv(archivo)

            # Eliminar .csv para limpiar (comentar en caso de querer mantener los .csv)
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
                    # Factor de ajuste para dar un poco de espacio adicional
                    ajuste = (longitud_maxima + 2) * 1.2
                    hoja_actual.column_dimensions[columna_cells[0].column_letter].width = ajuste

        print(f"Se han combinado los archivos en {dest} con hojas separadas.")

        # Confirmar los cambios y cerrar la conexión
        conn.commit()
        conn.close()

        print("Datos guardados en la Base de Datos")

except Exception as e:
    tb = traceback.format_exc()
    logging.error(f"Se ha producido un error: {e}\n{tb}")


