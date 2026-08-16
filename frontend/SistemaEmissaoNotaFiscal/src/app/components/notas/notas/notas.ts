import { Component } from '@angular/core';
import { PageHeader } from '../../shared/page-header/page-header';
import { PageShell } from '../../shared/page-shell/page-shell';
import { NovaNota } from '../nova-nota/nova-nota';
import { NotasEmitidas } from '../notas-emitidas/notas-emitidas';

@Component({
  selector: 'app-notas',
  imports: [PageHeader, PageShell, NovaNota, NotasEmitidas],
  templateUrl: './notas.html',
  styleUrl: './notas.css',
})
export class Notas {}
