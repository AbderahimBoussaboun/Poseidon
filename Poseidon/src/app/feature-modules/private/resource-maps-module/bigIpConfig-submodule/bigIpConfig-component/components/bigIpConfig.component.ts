import { Component, HostListener } from '@angular/core';
import { SidebarService } from './subcomponents/navbar/sidebar.service.spec'; // Asegúrate de importar desde la ubicación correcta

@Component({
  selector: 'app-bigIpConfig',
  templateUrl: './bigIpConfig.component.html',
  styleUrls: ['./bigIpConfig.component.css']
})
export class BigIpConfigComponent {
  title = 'poseidon-app';
  isSidebarOpen: boolean = false;

  constructor(public sidebarService: SidebarService) {
    this.sidebarService.isSidebarOpen$.subscribe(isOpen => {
      this.isSidebarOpen = isOpen;
    });
  }

  toggleSidebar(event: Event) {
    event.stopPropagation(); // Detiene la propagación del evento para que no se active el document:click
    this.sidebarService.toggleSidebar();
  }

  // Método para cerrar el sidebar al hacer clic fuera de él
  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent) {
    const sidebar = document.querySelector('.sidebar');
    if (sidebar) {
      const isClickedOutside = !sidebar.contains(event.target as Node);
      if (isClickedOutside && this.isSidebarOpen) {
        this.sidebarService.toggleSidebar();
      }
    }
  }
}
