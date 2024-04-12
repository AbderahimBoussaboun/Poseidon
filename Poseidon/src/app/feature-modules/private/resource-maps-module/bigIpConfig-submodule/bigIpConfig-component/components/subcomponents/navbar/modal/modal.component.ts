import { Component, ElementRef, ViewChild, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { BalancerService } from '../../../../../../balancers-submodule/balancer-component/services/balancer.service';

@Component({
  selector: 'app-navbar-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css', '../../../bigIpConfig.component.css']
})
export class NavbarModalComponent implements OnInit, OnDestroy {
  @ViewChild('fileInput', {static: false}) myInputVariable!: ElementRef;
  archivoSeleccionado: File | null = null;
  mensajeExito: string = '';
  mensajeError: string = '';
  selectedBalanceador: string = ''; // Variable para almacenar el balanceador seleccionado
  commitMessage: string = ''; // Variable para almacenar el mensaje de commit

  isSubmitting: boolean = false;
  private destroy$ = new Subject<void>();
  balancers: any[] = []; // Asumiendo que la respuesta es un array de objetos

  constructor(private http: HttpClient, private balancerService: BalancerService) {}

  ngOnInit(): void {
    // Aquí puedes inicializar cualquier lógica necesaria al cargar el componente
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  handleFileInput(files: FileList): void {
    this.archivoSeleccionado = files.item(0);
  }

  getAllBalancers() {
    this.balancerService.getAllBalancers()
      .subscribe(response => {
        console.log(response);
        if (Array.isArray(response)) {
          this.balancers = response; // La respuesta ya es un array de objetos
        } else {
          // Aquí puedes manejar la respuesta si no es un array
        }
      });
  }

  submitFile(): void {
    if (!this.selectedBalanceador || !this.commitMessage) {
      this.mensajeError = 'Debe seleccionar un balanceador y escribir un mensaje de commit.';
      return;
    }
    if (this.archivoSeleccionado) {
      const formData: FormData = new FormData();
      formData.append('archivo', this.archivoSeleccionado, this.archivoSeleccionado.name);
      formData.append('balancerName', this.selectedBalanceador);
      formData.append('commitMessage', this.commitMessage);
      this.isSubmitting = true;
      this.http.post('http://cpdcrprems02.idc.local/PoseidonApi/api/ResourceMaps/F5', formData, { responseType: 'text' })
        .pipe(takeUntil(this.destroy$))
        .subscribe(
          response => {
            this.mensajeExito = 'Archivo enviado y procesado con éxito.';
            this.mensajeError = '';
            this.isSubmitting = false;
            this.limpiarInputArchivo();
          },
          error => {
            this.mensajeError = 'Error al enviar el archivo.';
            this.mensajeExito = '';
            this.isSubmitting = false;
            this.limpiarInputArchivo();
          }
        );
    } else {
      this.mensajeError = 'No se ha seleccionado ningún archivo.';
    }
  }
  

  limpiarInputArchivo(): void {
    if (this.myInputVariable) {
      this.myInputVariable.nativeElement.value = '';
    }
    this.archivoSeleccionado = null;
  }

  // Método para limpiar el modal cuando se cierra
  onModalClose(): void {
    this.limpiarInputArchivo();
    this.mensajeExito = '';
    this.mensajeError = '';
  }
}
