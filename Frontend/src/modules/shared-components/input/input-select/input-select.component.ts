import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ControlContainer, Form, FormControl, FormGroup, FormGroupDirective } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { ClickOutsideDirective } from '@appModule/helpers/clickOutside.directive';
@Component({
  selector: 'input-select',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule,ClickOutsideDirective],
  templateUrl: './input-select.component.html',
  styleUrls: ['./input-select.component.scss']
})
export class InputSelectComponent {
  @Input() icon: any;
  @Input() label: string = '';
  @Input() validationMessage: string = '';
  @Output() valueChangeEvent = new EventEmitter<{ id: string, name: string }>();
  @Input() control: FormControl;
  @Input() optionList: { id: string, name: string }[];

  selectedOption?: { id: string, name: string };
  showOptions: boolean = false;
  onValueChange = (value: { id: string, name: string }) => {
    this.selectedOption = value;
    this.valueChangeEvent.emit(value);
    this.showOptions = false;
  }
  toggleOptions($event:MouseEvent) {
    $event.stopPropagation();
    this.showOptions = !this.showOptions;
    console.log(this.showOptions);
  }
  closeOptions() {
    // this.showOptions = false;
    console.log('yey')
  }
  constructor(
  ) { }

}
