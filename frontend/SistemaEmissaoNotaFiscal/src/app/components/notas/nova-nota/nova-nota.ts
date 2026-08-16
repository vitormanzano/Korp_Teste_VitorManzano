import { Component, OnInit, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { finalize } from 'rxjs';
import { Pill } from '../../shared/pill/pill';
import { Produto, ProdutosService } from '../../../services/produtos.service';
import { Nota, NotasService } from '../../../services/notas.service';

interface ItemNota {
  codigo: string;
  descricao: string;
  quantidade: number;
}

@Component({
  selector: 'app-nova-nota',
  imports: [ReactiveFormsModule, Pill],
  templateUrl: './nova-nota.html',
  styleUrl: './nova-nota.css',
})
export class NovaNota implements OnInit {
  private produtosService = inject(ProdutosService);
  private notasService = inject(NotasService);

  produtosDisponiveis = signal<Produto[]>([]);
  itens = signal<ItemNota[]>([]);
  notaCriada = signal<Nota | null>(null);
  saving = signal(false);
  errorMsg = signal<string | null>(null);

  itemForm = new FormGroup({
    codigo: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    quantidade: new FormControl(1, {
      validators: [Validators.required, Validators.min(1)],
      nonNullable: true,
    }),
  });

  ngOnInit(): void {
    this.produtosService.listar().subscribe({
      next: (produtos) => this.produtosDisponiveis.set(produtos),
    });
  }

  adicionarItem(): void {
    if (this.itemForm.invalid) {
      this.itemForm.markAllAsTouched();
      return;
    }

    const { codigo, quantidade } = this.itemForm.getRawValue();
    if (this.itens().some((item) => item.codigo === codigo)) {
      return;
    }

    const produto = this.produtosDisponiveis().find((p) => p.codigo === codigo);
    if (!produto) {
      return;
    }

    this.itens.update((itens) => [
      ...itens,
      { codigo, descricao: produto.descricao, quantidade },
    ]);
    this.itemForm.reset({ codigo: '', quantidade: 1 });
  }

  removerItem(codigo: string): void {
    this.itens.update((itens) => itens.filter((item) => item.codigo !== codigo));
  }

  criarNota(): void {
    if (this.itens().length === 0) {
      this.errorMsg.set('Adicione ao menos um produto antes de criar a nota.');
      return;
    }

    this.saving.set(true);
    this.errorMsg.set(null);
    this.notasService
      .criar(this.itens().map(({ codigo, quantidade }) => ({ codigoProduto: codigo, quantidade })))
      .pipe(finalize(() => this.saving.set(false)))
      .subscribe({
        next: (nota) => {
          this.notaCriada.set(nota);
          this.itens.set([]);
        },
        error: () =>
          this.errorMsg.set(
            'Serviço de Faturamento indisponível no momento. Tente novamente em instantes.',
          ),
      });
  }
}
