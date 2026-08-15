import { Component } from '@angular/core';
import { PageHeader } from '../../shared/page-header/page-header';
import { PageShell } from '../../shared/page-shell/page-shell';

@Component({
  selector: 'app-produtos',
  imports: [PageHeader, PageShell],
  templateUrl: './produtos.html',
  styleUrl: './produtos.css',
})
export class Produtos {}
