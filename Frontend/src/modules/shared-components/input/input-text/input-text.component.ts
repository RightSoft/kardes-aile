import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ControlContainer, Form, FormControl, FormGroup, FormGroupDirective } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { FormControlName } from '@angular/forms';
@Component({
  selector: 'input-text',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './input-text.component.html',
  styleUrls: ['./input-text.component.scss']
})
export class InputTextComponent  {

  @Input() icon: any;
  @Input() label: string = '';
  @Input() validationMessage: string = '';
  @Output() valueChangeEvent = new EventEmitter<string>();
  @Input() control: FormControl;


  value: string = '';

  onValueChange = (value: string) => {
    this.valueChangeEvent.emit(value);
  }

  constructor(
  ){}

}
