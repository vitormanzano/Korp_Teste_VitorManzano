import { Component, OnInit, inject, signal } from '@angular/core';
import { Pill, PillVariant } from '../../shared/pill/pill';
import { Nota, NotasService } from '../../../services/notas.service';

@Component({
  selector: 'app-notas-emitidas',
  imports: [Pill],
  templateUrl: './notas-emitidas.html',
  styleUrl: './notas-emitidas.css',
})
export class NotasEmitidas implements OnInit {
  private notasService = inject(NotasService);

  notas = signal<Nota[]>([]);
  loading = signal(true);
  errorMsg = signal<string | null>(null);
  imprimindo = signal<string | null>(null);
  falhaImpressao = signal<{ numero: string; motivo: string } | null>(null);

  ngOnInit(): void {
    this.notasService.listar().subscribe({
      next: (notas) => {
        this.notas.set(notas);
        this.loading.set(false);
      },
      error: () => {
        this.errorMsg.set('Não foi possível carregar as notas fiscais. Tente novamente em instantes.');
        this.loading.set(false);
      },
    });
  }

  statusVariant(status: Nota['status']): PillVariant {
    return status === 'Fechada' ? 'closed' : 'open';
  }

  imprimir(nota: Nota): void {
    if (nota.status !== 'Aberta' || this.imprimindo()) {
      return;
    }

    this.imprimindo.set(nota.numero);
    this.falhaImpressao.set(null);
    this.notasService.imprimir(nota.numero).subscribe({
      next: (notaAtualizada) => {
        this.notas.update((notas) =>
          notas.map((n) => (n.numero === notaAtualizada.numero ? notaAtualizada : n)),
        );
        this.imprimindo.set(null);
      },
      error: () => {
        this.falhaImpressao.set({
          numero: nota.numero,
          motivo:
            'Serviço de estoque indisponível no momento. A nota permanece Aberta — nenhum saldo foi debitado. Tente novamente em instantes.',
        });
        this.imprimindo.set(null);
      },
    });
  }
}
