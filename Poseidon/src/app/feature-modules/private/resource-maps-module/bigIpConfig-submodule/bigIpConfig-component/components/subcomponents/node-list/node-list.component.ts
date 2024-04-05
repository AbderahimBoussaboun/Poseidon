import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { VirtualServer, Node, Rule, IRule, Pool, NodePool } from '../../../models/entities'; // Asume que estas interfaces se definen en este archivo


@Component({
  selector: 'app-node-list',
  templateUrl: './node-list.component.html',
  styleUrls: ['./node-list.component.css', '../../bigIpConfig.component.css']
})
export class NodeListComponent implements OnInit{
  title="PoseidonProject";
  nodes: Node[] = [];  // Aquí se declara y se inicializa un array vacío.
  loading = true;
  searchText: string = '';

  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    this.http
      .get<Node[]>('http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/Nodes') // Especifica el tipo de dato que esperas recibir
      .subscribe(
        (data: Node[]) => {
          this.nodes = data; // Asigna los datos recibidos al array virtualServers
          this.loading = false; // Oculta la rueda de carga después de completar la operación
        },
        error => {
          console.error('There was an error!', error)
          this.loading = false; // Oculta la rueda de carga si ocurre un error
        }
      );
  }

  // Getter para la lista de servidores Nodes filtrados
  get filteredNodes(): Node[] {
    return this.nodes.filter(node =>
      node.name.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }

  preventPropagation(event: Event) {
    event.stopPropagation();
  }

  togglePoolDetails(poolItem: Pool) {
    this.nodes.forEach((item) => {
      if (item.nodePools && item.nodePools.find(pool => pool.pool === poolItem)) {
        item.nodePools.forEach(pool => {
          if (pool.pool === poolItem) {
            pool.pool.showDetails = !pool.pool.showDetails;
          } else {
            pool.pool.showDetails = false;
          }
        });
      }
    });

    if(!poolItem.showDetails){
      this.resetShowProperties(poolItem);
    }
    /*if (!pool.showDetails && pool.monitor){
        pool.monitor.showDetails=false;
        pool.showMembers=false;
      } */
  }  

  toggleDetails(nodeItem: Node) {
    this.nodes.forEach((item) => {
      if (item === nodeItem) {
        item.showDetails = !item.showDetails;
        item.isExpanded = !item.isExpanded;
      } else {
        item.showDetails = false;
        item.isExpanded = false; // Asegúrate de que todos los demás elementos estén colapsados
      }
    });
  if (!nodeItem.showDetails){
    this.resetShowProperties(nodeItem);
  }}

  togglePoolDetailsForAll(nodeItem: Node) {
    nodeItem.showPools=!nodeItem.showPools;
    if (nodeItem.nodePools) {
      for (const nodePool of nodeItem.nodePools) {
        this.resetShowProperties(nodePool.pool);
      }
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
  
}
