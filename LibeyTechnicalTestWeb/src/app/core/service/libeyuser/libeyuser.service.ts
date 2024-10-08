import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { LibeyUser } from 'src/app/entities/libeyuser';
import { ILibeyUserPagination } from 'src/app/entities/pagination';
import { IResult, IResultGeneric, IResultList } from 'src/app/entities/result';
@Injectable({
  providedIn: 'root',
})
export class LibeyUserService {
  constructor(private http: HttpClient) {}

  Find(documentNumber: string): Observable<IResultGeneric<LibeyUser>> {
    const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/${documentNumber}`;
    return this.http.get<IResultGeneric<LibeyUser>>(uri);
  }

  FindAll(
    paginationInput: ILibeyUserPagination
  ): Observable<IResultList<LibeyUser>> {
    const uri = `${environment.pathLibeyTechnicalTest}LibeyUser`;
    let params = new HttpParams()
      .set('Page', paginationInput.page.toString())
      .set('PageSize', paginationInput.pageSize.toString())
      .set('OrderBy', paginationInput.orderBy)
      .set('OrderDirection', paginationInput.orderDirection);

    if (paginationInput.textSearch) {
      console.log(paginationInput.textSearch);
      params = params.set('TextSearch', paginationInput.textSearch);
    }

    if (paginationInput.documentNumber) {
      params = params.set('DocumentNumber', paginationInput.documentNumber);
    }

    if (paginationInput.email) {
      params = params.set('Email', paginationInput.email);
    }

    return this.http.get<IResultList<LibeyUser>>(uri, { params });
  }

  Create(libeyUser: LibeyUser): Observable<IResult> {
    const uri = `${environment.pathLibeyTechnicalTest}LibeyUser`;
    return this.http.post<IResult>(uri, libeyUser);
  }

  Update(libeyUser: LibeyUser): Observable<IResult> {
    const uri = `${environment.pathLibeyTechnicalTest}LibeyUser`;
    return this.http.put<IResult>(uri, libeyUser);
  }

  Delete(documentNumber: string): Observable<IResult> {
    const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/${documentNumber}`;
    return this.http.delete<IResult>(uri);
  }
}
