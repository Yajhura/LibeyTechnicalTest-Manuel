import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ILibeyUbigeo } from 'src/app/entities/libeyUbigeo';
import { ILibeyUbigeoPagination } from 'src/app/entities/pagination';
import { IResultList } from 'src/app/entities/result';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LibeyUbigeoService {

	constructor(private http: HttpClient) {}

  FindAll(paginationInput:ILibeyUbigeoPagination): Observable<IResultList<ILibeyUbigeo>> {
    const uri = `${environment.pathLibeyTechnicalTest}LibeyUbigeo`;
    console.log(paginationInput);

    let params = new HttpParams()
    .set('Page', paginationInput.page.toString())
    .set('PageSize', paginationInput.pageSize.toString())
    .set('OrderBy', paginationInput.orderBy)
    .set('OrderDirection', paginationInput.orderDirection);

  if (paginationInput.code) {
      params = params.set('Code', paginationInput.code);
  }
    return this.http.get<IResultList<ILibeyUbigeo>>(uri, {params});
  }
}
