import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { VirtualServer, Node, Rule, IRule, Pool, NodePool } from '../../../models/entities'; // Asume que estas interfaces se definen en este archivo
@Component({
  selector: 'app-pool-list',
  templateUrl: './pool-list.component.html',
  styleUrls: ['./pool-list.component.css', '../../bigIpConfig.component.css']
})
export class PoolListComponent implements OnInit{
  title="PoseidonProject";
  pools: Pool[] = [];  // Aquí se declara y se inicializa un array vacío.
  loading = true;
  searchText: string = '';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http
      .get<Pool[]>('http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Pools') // Especifica el tipo de dato que esperas recibir
      .subscribe(
        (data: Pool[]) => {
          this.pools = data; // Asigna los datos recibidos al array virtualServers
          this.loading = false; // Oculta la rueda de carga después de completar la operación
        },
        error => {
          console.error('There was an error!', error)
          this.loading = false; // Oculta la rueda de carga si ocurre un error
        }
      );
      }

      // Getter para la lista de Pools filtradas
  get filteredPools(): Pool[] {
    return this.pools.filter(pool =>
      pool.name.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }

      preventPropagation(event: Event) {
        event.stopPropagation();
      }

      toggleDetails(pool: Pool) {
        this.pools.forEach((item) => {
          if (item === pool) {
            item.showDetails = !item.showDetails;
          } else {
            item.showDetails = false;
          }
        });
        if (!pool.showDetails) {
          this.resetShowProperties(pool);
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

      toggleMemberList(pool: Pool) {
        pool.showMembers = !pool.showMembers;
        if (!pool.showMembers) {
          // Resetea el selectedMember a undefined
          pool.selectedMember = undefined;
        }
      }

      showMemberDetails(selectedMember: Node, pool: Pool) {
        // Si el mismo miembro ya está seleccionado, limpiar selectedMember
        if (pool.selectedMember === selectedMember) {
          pool.selectedMember = undefined;
        } else {
          pool.selectedMember = selectedMember;
        }
      }

      toggleIRuleDetails(irule: IRule) {
        irule.showDetails = !irule.showDetails;
        if(!irule.showDetails){
          this.resetShowProperties(irule);
        }
      }

      toggleRuleDetails(rule: Rule, event: Event) {
        event.stopPropagation();
        rule.showDetails = !rule.showDetails;
        if(!rule.showDetails){
          this.resetShowProperties(rule);
        }
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

      toggleVirtualDetails(virtual: VirtualServer){
        virtual.showDetails= !virtual.showDetails;
        console.log(virtual.showDetails)
      }

      toggleVirtualsDetails(item: Pool){
        item.showVirtuals = !item.showVirtuals;
        if (!item.showVirtuals){
          item.virtuals.forEach((item) => {
            this.resetShowProperties(item);
          });
        }
      }

      toggleMonitorDetails(pool: Pool){
        pool.showMonitor= !pool.showMonitor;
      }

}
