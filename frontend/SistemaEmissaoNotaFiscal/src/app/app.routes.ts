import { Routes } from '@angular/router';
import { Produtos } from './components/produtos/produtos/produtos';
import { Notas } from './components/notas/notas/notas';

export const routes: Routes = [
  {
    path: 'produtos',
    component: Produtos,
  },
  {
    path: 'notas',
    component: Notas,
  },
];
