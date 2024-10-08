import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { LibeyUser } from 'src/app/entities/libeyuser';
import { ILibeyUserPagination } from 'src/app/entities/pagination';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css'],
})
export class UserlistComponent implements OnInit {
  users: LibeyUser[] = [];

  documentNumber: string = '';
  email: string = '';
  page: number = 1;
  pageSize: number = 10;
  total: number = 0;
  orderBy: string = 'DocumentNumber';
  orderDirection: string = 'asc';
  textSearch: string = '';
  totalPages: number = 0;

  constructor(
    private _service: LibeyUserService,
    private _router: Router
  ) {}

  ngOnInit(): void {
    this.LoadData();
  }

  LoadData() {
    const paginationInput: ILibeyUserPagination = {
      documentNumber: this.documentNumber,
      email: this.email,
      page: this.page,
      pageSize: this.pageSize,
      orderBy: this.orderBy,
      orderDirection: this.orderDirection,
      textSearch: this.textSearch,
    };
    console.log(paginationInput);

    this._service.FindAll(paginationInput).subscribe((data) => {
      if (data.isSuccess) {
        this.users = data.value;
        this.total = data.total;
        this.totalPages = Math.ceil(this.total / this.pageSize);

      } else {
        this.users = [];
        this.total = 0;
        this.totalPages = 0;

        Swal.fire('Error', data.message, 'error');
      }
    });
  }

  Search() {
    this.LoadData();
    this.page = 1;
  }

  NextPage() {
    if (this.page + 1 > Math.ceil(this.total / this.pageSize)) return;
    this.page++;
    this.LoadData();
  }

  PreviousPage() {
    if (this.page - 1 < 1) return;
    this.page--;
    this.LoadData();
  }

  ChangePageSize(pageSize: number) {
    this.pageSize = pageSize;
    this.page = 1;
    this.LoadData();
  }

  Edit(documentNumber: string) {
    this._router.navigate(['user/maintenance', documentNumber]);
  }

  Delete(documentNumber: string) {
    Swal.fire({
      title: '¿Estás seguro?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí',
    }).then((result) => {
      if (result.isConfirmed) {
        this._service.Delete(documentNumber).subscribe((data) => {
          if (data.isSuccess) {
            Swal.fire('Success', data.message, 'success');
            this.LoadData();
          } else {
            Swal.fire('Error', data.message, 'error');
          }
        });
      }
    });
  }

  CleanValues() {
    this.documentNumber = '';
    this.email = '';
    this.page = 1;
    this.pageSize = 10;
    this.total = 0;
    this.orderBy = 'DocumentNumber';
    this.orderDirection = 'asc';
    this.textSearch = '';
  }

  Back() {
    this._router.navigate(['user']);
  }
}
