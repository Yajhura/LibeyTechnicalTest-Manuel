
export interface IPagination {
  textSearch: string;
  page: number;
  pageSize: number;
  orderBy: string;
  orderDirection: string;
}

export interface ILibeyUbigeoPagination extends IPagination {
  code: string;
}

export interface ILibeyUserPagination extends IPagination {
  documentNumber: string;
  email :string;
}

