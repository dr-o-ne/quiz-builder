import { Component, Input } from '@angular/core';
import { OptionItem } from 'src/app/_models/UI/optionItem';

@Component({
  selector: 'app-options-menu',
  templateUrl: './options-menu.component.html',
  styleUrls: ['./options-menu.component.css']
})
export class OptionsMenuComponent {

  @Input() options: OptionItem[];

  onOptionItemClick(event: MouseEvent, option: OptionItem): void {
    option.enabled = !option.enabled;
    event.stopPropagation();
  }

}
