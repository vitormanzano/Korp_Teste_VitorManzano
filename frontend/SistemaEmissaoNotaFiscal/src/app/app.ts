import { Component, signal } from '@angular/core';
import { Header } from './components/shared/header/header';
import { Produtos } from './components/produtos/produtos/produtos';

@Component({
  selector: 'app-root',
  imports: [Header, Produtos],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('SistemaEmissaoNotaFiscal');
}
