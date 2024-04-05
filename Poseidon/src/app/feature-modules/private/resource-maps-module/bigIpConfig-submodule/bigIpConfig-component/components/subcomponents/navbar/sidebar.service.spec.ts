import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  private sidebarOpen = new BehaviorSubject<boolean>(false);

  // Observable para acceder al estado actual del sidebar
  public isSidebarOpen$ = this.sidebarOpen.asObservable();

  // Método para cambiar el estado
  public toggleSidebar() {
    this.sidebarOpen.next(!this.sidebarOpen.value);
  }
}
