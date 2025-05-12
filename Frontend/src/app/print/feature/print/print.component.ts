import { Component } from '@angular/core';
import { PdfService } from '../../data-access/pdf.service';
import { Router } from '@angular/router';
import { routes } from '../../../app.routes';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-print',
  standalone: true,
  imports: [CommonModule, NzInputModule, NzCardModule, NzInputNumberModule, FormsModule],
  templateUrl: './print.component.html',
  styleUrl: './print.component.css'
})
export class PrintComponent {
  advertId: number | null = null;

  constructor(private pdfService: PdfService, private router: Router) {}

  printPdf(): void {
    if (!this.advertId || this.advertId <= 0) {
      return;
    }

    this.pdfService.getPdfBlob(this.advertId).subscribe((blob) => {
      const blobUrl = URL.createObjectURL(blob);
      const printWindow = window.open(blobUrl);
      if (printWindow) {
        printWindow.addEventListener('load', () => {
          printWindow.focus();
          printWindow.print();
        });
      }
    });
  }
}
