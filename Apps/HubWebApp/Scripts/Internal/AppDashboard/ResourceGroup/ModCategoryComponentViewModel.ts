import * as ko from 'knockout';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { AlertViewModel } from 'XtiShared/Alert';
import * as template from './ModCategoryComponent.html';
import { SimpleEvent } from 'XtiShared/Events';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';

export class ModCategoryComponentViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('resource-group-mod-category-component', template));
    }

    readonly alert = new AlertViewModel();
    readonly name = ko.observable('');

    private readonly _clicked = new SimpleEvent(this);
    readonly clicked = this._clicked.handler();

    click() {
        this._clicked.invoke();
    }
}