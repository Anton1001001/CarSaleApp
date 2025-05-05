import { Injectable } from '@angular/core';
import { CategoryResponse } from './models/category-response';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../../environments/environment.development';

export interface GetCarAdvertFormResponse {
  brands?: BrandResponse[];
  models?: ModelResponse[];
  years?: YearResponse[];
  generations?: GenerationResponse[];
  bodyTypes?: BodyTypeResponse[];
  transmissionTypes?: TransmissionTypeResponse[];
  engineTypes?: EngineTypeResponse[];
  driveTypes?: DriveTypeResponse[];
  modifications?: ModificationResponse[];
  colors?: ColorResponse[];
  interiorColors?: InteriorColorResponse[];
  interiorMaterials?: InteriorMaterialResponse[];
  placeRegions?: PlaceRegionsResponse[];
  placeCities?: PlaceCitiesResponse[];
  phoneCodes?: PhoneCodeResponse[]
}

export interface YearResponse {
  year: number;
}

export interface PhoneCodeResponse {
  id: number;
  emoji: string;
  code: string;
}

export interface PlaceRegionsResponse {
  id: number;
  name: string;
}

export interface PlaceCitiesResponse {
  id: number;
  name: string;
}

export interface InteriorMaterialResponse {
  id: number;
  name: string;
}

export interface InteriorColorResponse {
  id: number;
  name: string;
}

export interface ColorResponse {
  id: number;
  name: string; 
}

export interface ModificationResponse {
  id: number;
  name: string;
}

export interface DriveTypeResponse {
  id: number;
  name: string;
}

export interface EngineTypeResponse {
  id: number;
  name: string;
}

export interface TransmissionTypeResponse {
  id: number;
  name: string;
}

export interface BodyTypeResponse {
  id: number;
  name: string;
}

export interface BrandResponse {
  id: number;
  name: string;
}

export interface ModelResponse {
  id: number;
  name: string;
}

export interface YearResponse {
  year: number;
}

export interface GenerationResponse {
  id: number;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class AdvertService {

  constructor(private http: HttpClient) {}

  getCategories(): Observable<CategoryResponse[]> {
    return this.http.get<CategoryResponse[]>(`${environment.appUrl}/api/adverts/categories`);
  }

  createAdvert(payload: any): Observable<any> {
    return this.http.post<any>(`${environment.appUrl}/api/adverts/cars/create`, payload);
  }

  getAdvertForm(parameters: any): Observable<GetCarAdvertFormResponse> {
    return this.http.post<GetCarAdvertFormResponse>(`${environment.appUrl}/api/adverts/cars/form`, parameters);
  }
  
  getAdverts() {
    return this.http.get<any[]>(`${environment.appUrl}/api/adverts`);
  }

  getAdvertById(id: number) {
    return this.http.get<any>(`${environment.appUrl}/api/adverts/${id}`);
  }
}

export interface Advert {
  id: number,
  description: string
}
