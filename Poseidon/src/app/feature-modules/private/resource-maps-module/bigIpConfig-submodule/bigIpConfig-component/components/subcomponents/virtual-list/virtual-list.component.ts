import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { VirtualServer, Node, Rule, IRule } from '../../../models/entities'; // Asume que estas interfaces se definen en este archivo


@Component({
  selector: 'app-virtual-list',
  templateUrl: './virtual-list.component.html',
  styleUrls: ['./virtual-list.component.css', '../../bigIpConfig.component.css']
})
export class VirtualListComponent implements OnInit{
  title="PoseidonProject";
  virtualServers: VirtualServer[] = [];  
  loading = true; //
  searchText: string = '';

  constructor(private http: HttpClient) {}
 
  ngOnInit(): void {
    this.http
      .get<VirtualServer[]>('http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Virtuals')
      .subscribe(
        (data: VirtualServer[]) => {
          this.virtualServers = data; 
          this.loading = false; // Oculta la rueda de carga después de completar la operación
        },
        error => {
          console.error('There was an error!', error)
          this.loading = false; // Oculta la rueda de carga si ocurre un error
        }
      );
  }

  // Getter para la lista de servidores virtuales filtrados
  get filteredVirtualServers(): VirtualServer[] {
    return this.virtualServers.filter(server =>
      server.name.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }

  toggleDetails(selectedItem: VirtualServer, event: Event) {
    event.stopPropagation();  // Evita la propagación del clic
    this.virtualServers.forEach((item) => {
      if (item === selectedItem) {
        item.showDetails = !item.showDetails;
        item.isExpanded = !item.isExpanded;
      } else {
        item.showDetails = false;
        item.isExpanded = false; // Asegúrate de que todos los demás elementos estén colapsados
      }
      // Agrega las siguientes líneas para cerrar todos los sub-desplegables
      if (!item.showDetails) {
        this.resetShowProperties(item);  // reset properties if hiding details
        item.showPoolDetails = false;
        item.showMemberList = false;
        item.showMonitorDetails = false;            
        if (item.rules) {
          item.rules.forEach(rule => rule.showDetails = false);
        }
        item.selectedMember = undefined;  // cierra cualquier miembro seleccionado
      }
    });
  }
  
  togglePoolDetails(virtualServer: VirtualServer) {
    if (virtualServer.pool?.name) {
      virtualServer.showPoolDetails = !virtualServer.showPoolDetails;
      if (!virtualServer.showPoolDetails && virtualServer.pool) {
        // Cierra los detalles del monitor si están abiertos
        virtualServer.showMonitorDetails = false;
        // Cierra la lista de miembros si está abierta
        virtualServer.showMemberList = false;
      }
      virtualServer.selectedMember = undefined;
    }
  }
  
  toggleMemberList(virtualServer: VirtualServer) {   
    virtualServer.showMemberList = !virtualServer.showMemberList;
    if(!virtualServer.showMemberList) {
       // Asegúrate de que 'nodePools' existe antes de intentar usarlo
       if(virtualServer.pool && virtualServer.pool.nodePools) {
           // Si 'nodePools' es un array, debes recorrerlo con un loop
           virtualServer.pool.nodePools.forEach(nodePool => {
             // Suponiendo que el nodePool en sí es el objeto 'member'
             this.resetShowProperties(nodePool);
           });
       }
       // Resetea el selectedMember a undefined
       virtualServer.selectedMember = undefined;
    }
}
  
  toggleMonitorDetails(virtualServer: any) {
    virtualServer.showMonitorDetails = !virtualServer.showMonitorDetails;
  }
  
  toggleRuleList(virtualServer: VirtualServer) {
    if (virtualServer.rules && virtualServer.rules.length > 0) {
      virtualServer.showRuleList = !virtualServer.showRuleList;
      if (!virtualServer.showRuleList) {
        // Resetea las propiedades de cada regla en 'rules'
        virtualServer.rules.forEach(rule => {
          this.resetShowProperties(rule);
        });
      }
    }
  }
  
  
  
  toggleRuleDetails(rule: Rule, event: Event) {
    event.stopPropagation();
    rule.showDetails = !rule.showDetails;
    if(!rule.showDetails){
      this.resetShowProperties(rule);
    }
  }
  
  toggleIRuleDetails(irule: IRule) {
    irule.showDetails = !irule.showDetails;
    if(!irule.showDetails){
      this.resetShowProperties(irule);
    }
  }
  
  resetShowProperties(item: any) {
    for (let key in item) {
      if (key.startsWith('show')) {
        item[key] = false;  
      } else if (item[key] instanceof Object) {
        this.resetShowProperties(item[key]);  
      }
    }
  }
  
  // Nueva función para evitar la propagación del clic
  preventPropagation(event: Event) {
    event.stopPropagation();
  }

 showMemberDetails(selectedMember: Node, virtualServer: VirtualServer) {
    // Si el mismo miembro ya está seleccionado, limpiar selectedMember
    if (virtualServer.selectedMember === selectedMember) {
      virtualServer.selectedMember = undefined;
    } else {
      virtualServer.selectedMember = selectedMember;
    }
  }

  showRuleDetails(rule: Rule) {
    // Implementa la lógica para mostrar detalles de la regla aquí
    console.log('Detalles de la Regla:', rule);
  }
}
