

export interface IResult {
  isSuccess: boolean;
  isFailure: boolean;
  message: string;
}

export interface IResultGeneric<T> extends IResult {
  value: T;
}

export interface IResultList<T> extends IResult {
  value: T[];
  total: number;
}
