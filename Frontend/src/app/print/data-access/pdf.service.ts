import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class PdfService {
  constructor(private http: HttpClient) {}

  getPdfBlob(id: number): Observable<Blob> {
    return this.http.get(`http://localhost:5000/api/adverts/print/${id}`, {
      responseType: 'blob',
    });
  }
  
}
