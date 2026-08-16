import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';

export interface NotaItem {
  codigoProduto: string;
  descricaoProduto: string;
  quantidade: number;
}

export interface Nota {
  numero: string;
  status: 'Aberta' | 'Fechada';
  itens: NotaItem[];
}

@Service()
export class NotasService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:5186/notas';

  listar(): Observable<Nota[]> {
    return this.http.get<Nota[]>(this.baseUrl);
  }

  criar(itens: { codigoProduto: string; quantidade: number }[]): Observable<Nota> {
    return this.http.post<Nota>(this.baseUrl, { itens });
  }

  imprimir(numero: string): Observable<Nota> {
    return this.http.post<Nota>(`${this.baseUrl}/${numero}/imprimir`, {});
  }
}
