import swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { LibeyUbigeoService } from 'src/app/core/service/libeyUbigeo/libey-ubigeo.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LibeyUser } from 'src/app/entities/libeyuser';
import { Router, ActivatedRoute } from '@angular/router';
import { IResultList } from 'src/app/entities/result';
import { ILibeyUbigeo } from 'src/app/entities/libeyUbigeo';
@Component({
  selector: 'app-usermaintenance',
  templateUrl: './usermaintenance.component.html',
  styleUrls: ['./usermaintenance.component.css'],
})
export class UsermaintenanceComponent implements OnInit {
  documentNumberId: string = '';
  form: FormGroup;
  regions: ILibeyUbigeo[] = [];
  provinces: ILibeyUbigeo[] = [];
  districts: ILibeyUbigeo[] = [];

  constructor(
    private _service: LibeyUserService,
    private _serviceUbigeo: LibeyUbigeoService,
    private _formBuilder: FormBuilder,
    private _router: Router,
    private _activatedRoute: ActivatedRoute
  ) {
    this.form = this._formBuilder.group({
      documentTypeId: [null, Validators.required],
      documentNumber: [
        '',
        [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(20),
        ],
      ],
      name: ['', [Validators.required, Validators.maxLength(100)]],
      fathersLastName: ['', [Validators.required, Validators.maxLength(100)]],
      mothersLastName: ['', [Validators.required, Validators.maxLength(100)]],
      address: ['', [Validators.required, Validators.maxLength(200)]],
      regionCode: [null, Validators.required],
      provinceCode: [null, Validators.required],
      ubigeoCode: [null, Validators.required],
      phone: ['', [Validators.required, Validators.pattern(/^\d{9}$/)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      active: [false],
    });
  }

  ngOnInit(): void {
    this.loadRegions();

    this._activatedRoute.params.subscribe((params) => {
      this.documentNumberId = params['documentNumber'];
      if (this.documentNumberId) {
        this.single(this.documentNumberId);
      }
    });
  }

  Submit() {
    if (this.form.invalid) {
      swal.fire(
        'Oops!',
        'Por favor, complete los campos obligatorios',
        'error'
      );
      return;
    }

    const user: LibeyUser = this.form.value;
    if (this.documentNumberId) {
      this._service.Update(user).subscribe((data) => {
        if (data.isSuccess) {
          swal.fire('Success', data.message, 'success');
          this.cleanValues();
          this._router.navigate(['/user']);
        } else {
          swal.fire('Oops!', data.message, 'error');
        }
      });

    }else {
      this._service.Create(user).subscribe((data) => {
        if (data.isSuccess) {
          swal.fire('Success', data.message, 'success');
          this.cleanValues();
        } else {
          swal.fire('Oops!', data.message, 'error');
        }
      });
    }
  }

  loadRegions() {
    this._serviceUbigeo
      .FindAll({
        code: '',
        page: 1,
        pageSize: 100,
        orderBy: 'code',
        orderDirection: 'asc',
        textSearch: '',
      })
      .subscribe((data: IResultList<ILibeyUbigeo>) => {
        this.regions = data.value;
      });
  }

  loadProvinces() {
    const regionCode = this.form.get('regionCode')?.value || '';
    this._serviceUbigeo
      .FindAll({
        code: regionCode,
        page: 1,
        pageSize: 100,
        orderBy: 'code',
        orderDirection: 'asc',
        textSearch: '',
      })
      .subscribe((data: IResultList<ILibeyUbigeo>) => {
        this.provinces = data.value;
      });
  }

  loadDistricts() {
    const provinceCode = this.form.get('provinceCode')?.value || '';
    this._serviceUbigeo
      .FindAll({
        code: provinceCode,
        page: 1,
        pageSize: 100,
        orderBy: 'code',
        orderDirection: 'asc',
        textSearch: '',
      })
      .subscribe((data: IResultList<ILibeyUbigeo>) => {
        this.districts = data.value;
      });
  }

  cleanValues() {
    this.form.reset();
    this.form.patchValue({ active: false });
  }

  single(documentNumber: string) {
    this._service.Find(documentNumber).subscribe((data) => {
      if (data.isSuccess) {
        this.form.patchValue({
          provinceCode: data.value.provinceCode,
          regionCode: data.value.regionCode,
        });
        this.loadProvinces();
        this.loadDistricts();
        this.form.patchValue(data.value);
      } else {
        swal.fire('Error', data.message, 'error');
      }
    });
  }

  Back() {
    this._router.navigate(['/user']);
  }
}
