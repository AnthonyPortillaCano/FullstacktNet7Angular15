import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { Compra } from '../models/compra';
import { CompraService } from './compra.service';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { DialogoConfirmacionComponent } from '../dialogo-confirmacion/dialogo-confirmacion.component';
import { Respuesta } from '../models/respuesta';
import { DialogAlertComponent } from '../dialog-alert/dialog-alert.component';
import * as XLSX from 'xlsx';
import jspdf from 'jspdf';
import autoTable from 'jspdf-autotable';
@Component({
  selector: 'app-compra-list',
  templateUrl: './compra-list.component.html',
  styleUrls: ['./compra-list.component.css']
})
export class CompraListComponent implements OnInit {
  displayedColumns:string[]=['id','numeroDocumento','razonSocial','total','editar','borrar'];
  data:Compra[]=[];
  isLoadingResults=true;
  dataSource:any;
  mensaje:string="";
  casesForm:FormGroup=new FormGroup({});
  @ViewChild(MatPaginator,{static:true}) paginator!:MatPaginator;
  constructor(private api:CompraService,public dialogo:MatDialog,private formBuilder:FormBuilder){}
  ngOnInit(): void {
       this.listarCompras('','');
      this.casesForm=this.formBuilder.group({
         numeroDocumento:[''],
         razonSocial:['']
      });
  }
   listarCompras(numeroDocumento:string,razonSocial:string)
   {
      this.api.ListCompras(numeroDocumento,razonSocial).subscribe({
        next:(res:Compra[])=>{
           this.data=res;
           this.isLoadingResults=false;
           this.dataSource=new MatTableDataSource<Compra>(this.data);
           this.dataSource.paginator=this.paginator
        },error:(e)=>{
           var error:any=localStorage.getItem("error");
            if(JSON.parse(error).status=="404")
            {
              this.isLoadingResults=false;
              this.data=[];
              this.dataSource=new MatTableDataSource<Compra>(this.data);
              this.dataSource.paginator=this.paginator
            }
        }
      })
   }
   buscar()
   {
     this.listarCompras(this.casesForm.value.numeroDocumento,this.casesForm.value.razonSocial);
   }
   limpiar()
   {
      this.casesForm.setValue({
        numeroDocumento:"",
        razonSocial:"",
      })
   }  
   borrarFila(id:number)
   {
     this.dialogo.open(DialogoConfirmacionComponent,{data: `¿Estas seguro de eliminar la informacion de la compra?`})
     .afterClosed().subscribe((confirmado:boolean)=>{
        if(confirmado)
        {
           this.api.DeleteCompras(id).subscribe({
             next:(respuesta:Respuesta)=>{
                console.log(respuesta);
                this.mensaje="Se elimino correctamente la compra";
                this.mensajeAlerta(this.mensaje);
                location.reload();
             },error:(error)=>{
               console.log(error);
             }
           })
        }
        else
        {
          console.log("cancelar");
        }
     })
   }  
   mensajeAlerta(mensaje:string){
    this.mensaje=mensaje;
    const dialogRef=this.dialogo.open(DialogAlertComponent,{
      width:'250px',
      data:{mensaje:this.mensaje}
    })
    dialogRef.afterClosed().subscribe(result=>{
      console.log('The dialog was closed');
    })
   }
   descargarExcel()
   {
     const datos=this.data.map(compras=>{
      return {
        NumeroDocumento:compras.numeroDocumento,
        RazonSocial:compras.razonSocial,
        Total:compras.total
      }});
      const wb=XLSX.utils.book_new();
      const ws=XLSX.utils.json_to_sheet(datos);
      XLSX.utils.book_append_sheet(wb,ws,'Compras');
      XLSX.writeFile(wb,"ListaCompras.xlsx");
   }
   exportarPdf()
   {
     const doc=new jspdf();
     const head=[['id','numeroDocumento','razonSocial','total']];
     const rows=this.data.map(item=>[item.id,item.numeroDocumento,item.razonSocial,item.total]);
     autoTable(doc,{
      head:head,
      body:rows,
      didDrawCell:(rows)=>{},
     });
      doc.save("ListaCompras.pdf");
   }
}
