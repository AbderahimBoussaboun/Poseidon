import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { VirtualServer, Node, Rule, IRule, Pool, NodePool, Monitor } from '../../../models/entities'; // Asume que estas interfaces se definen en este archivo

@Component({
  selector: 'app-monitor-list',
  templateUrl: './monitor-list.component.html',
  styleUrls: ['./monitor-list.component.css', '../../bigIpConfig.component.css']
})
export class MonitorListComponent implements OnInit {
  title = "PoseidonProject";
  monitors: Monitor[] = [];  // Aquí se declara y se inicializa un array vacío.
  loading = true;
  searchText: string = '';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http
      .get<Monitor[]>('http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Monitors') // Especifica el tipo de dato que esperas recibir
      .subscribe(
        (data: Monitor[]) => {
          this.monitors = data; // Asigna los datos recibidos al array virtualServers
          this.loading = false; // Oculta la rueda de carga después de completar la operación
        },
        error => {
          console.error('There was an error!', error)
          this.loading = false; // Oculta la rueda de carga si ocurre un error
        }
      );
  }

  // Getter para la lista de servidores virtuales filtrados
  get filteredMonitors(): Monitor[] {
    return this.monitors.filter(monitor =>
      monitor.name.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }

  toggleDetails(itemMonitor: Monitor) {
    this.monitors.forEach((item) => {
      if (item === itemMonitor) {
        item.showDetails = !item.showDetails;
      } else {
        item.showDetails = false;
      }
    });
    if (!itemMonitor.showDetails) {
      this.resetShowProperties(itemMonitor);
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

  preventPropagation(event: Event) {
    event.stopPropagation();
  }

  togglePoolDetailsForAll(monitorItem: Monitor) {
    monitorItem.showPools = !monitorItem.showPools;

    if (monitorItem.pools) {
      for (const pool of monitorItem.pools) {
        this.resetShowProperties(pool);
      }

    }

    if (!monitorItem.showPools) {
      monitorItem.showMemberList = false;
      monitorItem.selectedMember = undefined;
    }
  }

  togglePoolDetails(poolItem: Pool) {
    this.monitors.forEach((item) => {
      if (item.pools && item.pools.includes(poolItem)) {
        item.pools.forEach(pool => {
          if (pool === poolItem) {
            pool.showDetails = !pool.showDetails;
            if (!pool.showDetails) {
              item.showMemberList = false;
              item.selectedMember = undefined;
              // Si deseas restablecer las propiedades de 'poolItem', llamarías a 'resetShowProperties(poolItem)' aquí
              // Por ejemplo:
              this.resetShowProperties(pool);
            }
          } else {
            this.resetShowProperties(pool);
          }
        });
      }
    });


    /* 
    if (!poolItem.showDetails && poolItem.monitor) {
      poolItem.monitor.showDetails = false;
      poolItem.showMembers = false;
    } 
    */
  }

  showMemberDetails(selectedMember: Node, monitorItem: Monitor) {
    // Si el mismo miembro ya está seleccionado, limpiar selectedMember
    if (monitorItem.selectedMember === selectedMember) {
      monitorItem.selectedMember = undefined;
    } else {
      monitorItem.selectedMember = selectedMember;
    }
  }

  toggleGeneralDetails(item: any) {
    item.showGeneralDetails = !item.showGeneralDetails;
  }

  toggleMemberList(monitorItem: Monitor) {
    monitorItem.showMemberList = !monitorItem.showMemberList;
    if (!monitorItem.showMemberList) {
      // Resetea el selectedMember a undefined
      monitorItem.selectedMember = undefined;
    }
  }

}
