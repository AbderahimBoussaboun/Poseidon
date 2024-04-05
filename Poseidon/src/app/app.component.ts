import { Component, HostListener } from '@angular/core';
import { SidebarService } from './feature-modules/private/resource-maps-module/bigIpConfig-submodule/bigIpConfig-component/components/subcomponents/navbar/sidebar.service.spec';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Poseidon';

  shouldShowScrollTopButton = false;

  constructor(public sidebarService: SidebarService) {}

  @HostListener('window:scroll', [])
  onWindowScroll() {
    // Calcula el desplazamiento vertical actual de la página
    const verticalOffset = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
    
    // Define un valor límite para mostrar el botón (por ejemplo, 500px)
    const scrollLimit = 500;

    // Actualiza el estado del botón basado en el desplazamiento vertical
    this.shouldShowScrollTopButton = verticalOffset >= scrollLimit;
  }

  scrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' }); // Desplaza suavemente al inicio de la página
  }
}
