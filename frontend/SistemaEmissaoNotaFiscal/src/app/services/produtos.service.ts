import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';

export interface Produto {
  codigo: string;
  descricao: string;
  saldo: number;
}

@Service()
export class ProdutosService {
  private http = inject(HttpClient);
  private baseUrl = '';

  criar(produto: Omit<Produto, 'saldo'> & { saldoInicial: number }): Observable<Produto> {
    return this.http.post<Produto>(this.baseUrl, produto);
  }

  listar() {
    return this.http.get<Produto[]>(this.baseUrl);
  }
}
