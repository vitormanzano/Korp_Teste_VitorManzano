import { Component, OnInit, inject, signal } from '@angular/core';
import { Pill, PillVariant } from '../../shared/pill/pill';
import { Produto, ProdutosService } from '../../../services/produtos.service';

const SALDO_BAIXO_LIMITE = 5;

@Component({
  selector: 'app-produtos-cadastrados',
  imports: [Pill],
  templateUrl: './produtos-cadastrados.html',
  styleUrl: './produtos-cadastrados.css',
})
export class ProdutosCadastrados implements OnInit {
  private produtosService = inject(ProdutosService);

  produtos = signal<Produto[]>([]);
  loading = signal(true);
  errorMsg = signal<string | null>(null);

  ngOnInit(): void {
    this.produtosService.listar().subscribe({
      next: (produtos) => {
        this.produtos.set(produtos);
        this.loading.set(false);
      },
      error: () => {
        this.errorMsg.set('Não foi possível carregar os produtos. Tente novamente em instantes.');
        this.loading.set(false);
      },
    });
  }

  statusFor(saldo: number): { variant: PillVariant; label: string } | null {
    if (saldo === 0) {
      return { variant: 'danger', label: 'Sem estoque' };
    }
    if (saldo <= SALDO_BAIXO_LIMITE) {
      return { variant: 'open', label: 'Estoque baixo' };
    }
    return null;
  }
}
