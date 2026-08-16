import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { Card } from '../../shared/card/card';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { debounceTime, finalize } from 'rxjs';
import { ProdutosService } from '../../../services/produtos.service';
import { HttpErrorResponse } from '@angular/common/http';

let initialDescriptonValue = '';
const savedForm = window.localStorage.getItem('saved-description-form');

if (savedForm) {
  const loadedForm = JSON.parse(savedForm);
  initialDescriptonValue = loadedForm.description;
}

@Component({
  selector: 'app-novo-produto',
  imports: [Card, ReactiveFormsModule],
  templateUrl: './novo-produto.html',
  styleUrl: './novo-produto.css',
})
export class NovoProduto implements OnInit {
  private produtosService = inject(ProdutosService);
  saving = signal(false);
  errorMsg = signal<string | null>(null);

  private destroyRef = inject(DestroyRef);
  form = new FormGroup({
    codigo: new FormControl('', {
      validators: [Validators.required],
      nonNullable: true,
    }),
    descricao: new FormControl(initialDescriptonValue, {
      validators: [Validators.required],
      nonNullable: true,
    }),
    saldoInicial: new FormControl(0, {
      validators: [Validators.required, Validators.min(0)],
      nonNullable: true,
    }),
  });

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.saving.set(true);
    this.errorMsg.set(null);
    this.produtosService
      .criar(this.form.getRawValue())
      .pipe(finalize(() => this.saving.set(false)))
      .subscribe({
        next: () => this.reset(),
        error: (err: HttpErrorResponse) =>
          this.errorMsg.set('Falha ao salvar produto. Tente novamente'),
      });
  }

  reset() {
    this.form.reset();
  }

  ngOnInit(): void {
    const subscription = this.form.valueChanges.pipe(debounceTime(500)).subscribe({
      next: (value) => {
        window.localStorage.setItem(
          'saved-description-form',
          JSON.stringify({ description: value.descricao }),
        );
      },
    });

    this.destroyRef.onDestroy(() => subscription.unsubscribe());
  }
}
