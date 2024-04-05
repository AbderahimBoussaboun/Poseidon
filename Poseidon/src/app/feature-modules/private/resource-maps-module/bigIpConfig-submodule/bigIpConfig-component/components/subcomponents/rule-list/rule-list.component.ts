import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { VirtualServer, Node, Rule, IRule, Pool, NodePool } from '../../../models/entities'; // Asume que estas interfaces se definen en este archivo

@Component({
  selector: 'app-rule-list',
  templateUrl: './rule-list.component.html',
  styleUrls: ['./rule-list.component.css', '../../bigIpConfig.component.css']
})
export class RuleListComponent implements OnInit{
  title="PoseidonProject";
  rules: Rule[] = [];  // Aquí se declara y se inicializa un array vacío.
  loading = true;
  searchText: string = '';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http
      .get<Rule[]>('http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Rules') // Especifica el tipo de dato que esperas recibir
      .subscribe(
        (data: Rule[]) => {
          this.rules = data; // Asigna los datos recibidos al array virtualServers
          this.loading = false; // Oculta la rueda de carga después de completar la operación
        },
        error => {
          console.error('There was an error!', error)
          this.loading = false; // Oculta la rueda de carga si ocurre un error
        }
      );
      }

      // Getter para la lista de rules filtradas
  get filteredRules(): Rule[] {
    return this.rules.filter(rule =>
      rule.name.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }

      preventPropagation(event: Event) {
        event.stopPropagation();
      }

      toggleIRuleDetails(irule: IRule) {
        irule.showDetails = !irule.showDetails;
        if(!irule.showDetails){
          this.resetShowProperties(irule);
        }
      }

      toggleVirtualDetails(rule: Rule){
        rule.showVirtualDetails=!rule.showVirtualDetails;
        if (!rule.showVirtualDetails){
          this.resetShowProperties(rule.virtual);
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

      toggleIrulesDetails(rule: Rule){
        rule.showIRules=!rule.showIRules;
        if (!rule.showIRules){
            rule.irules?.forEach((item)=>{
              this.resetShowProperties(item);
            });
        }
      }

      toggleMonitorDetails(virtualServer: any) {
        virtualServer.showMonitorDetails = !virtualServer.showMonitorDetails;
      }

      showMemberDetails(selectedMember: Node, virtualServer: VirtualServer) {
        // Si el mismo miembro ya está seleccionado, limpiar selectedMember
        if (virtualServer.selectedMember === selectedMember) {
          virtualServer.selectedMember = undefined;
        } else {
          virtualServer.selectedMember = selectedMember;
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
      
      toggleDetails(selectedItem: Rule) {
        this.rules.forEach((item) => {
          if (item === selectedItem) {
            item.showDetails = !item.showDetails;
          } else {
            item.showDetails = false;
          }
          // Agrega las siguientes líneas para cerrar todos los sub-desplegables
          if (!item.showDetails) {
            this.resetShowProperties(item);  // reset properties if hiding details
          }
        });
      }
}
