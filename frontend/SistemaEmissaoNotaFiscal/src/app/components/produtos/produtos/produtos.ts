import { Component } from '@angular/core';
import { PageHeader } from '../../shared/page-header/page-header';
import { PageShell } from '../../shared/page-shell/page-shell';
import { NovoProduto } from '../novo-produto/novo-produto';

@Component({
  selector: 'app-produtos',
  imports: [PageHeader, PageShell, NovoProduto],
  templateUrl: './produtos.html',
  styleUrl: './produtos.css',
})
export class Produtos {}
