import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css', '../../bigIpConfig.component.css']
})
export class NavbarComponent {
  sidebarClosed = false;
  archivoSeleccionado: File | null = null; // Cambio aquí

  constructor(private http: HttpClient) {}

  toggleSidebar() {
    this.sidebarClosed = !this.sidebarClosed;
  }

  handleFileInput(files: FileList) {
    this.archivoSeleccionado = files.item(0);
  }

  async submitFile() {
    if (this.archivoSeleccionado) {
      const formData: FormData = new FormData();
      formData.append('archivo', this.archivoSeleccionado, this.archivoSeleccionado.name);

      const headers = new HttpHeaders({
        'Content-Type': 'multipart/form-data'
      });

      try {
        const response = await this.http.post('http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/F5', formData, { headers: headers }).toPromise();
        console.log('Archivo enviado con éxito:', response);
      } catch (error) {
        console.error('Error al enviar el archivo:', error);
      }
    } else {
      console.error('No se ha seleccionado ningún archivo.');
    }
  }
}
