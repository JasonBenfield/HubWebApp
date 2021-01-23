import { AlertViewModel } from "XtiShared/Alert";
import { ComponentTemplate } from "XtiShared/ComponentTemplate";
import { ComponentViewModel } from "XtiShared/ComponentViewModel";
import * as ko from 'knockout';
import * as template from './ModCategoryComponent.html';

export class ModCategoryComponentViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('mod-category-component', template));
    }
    readonly alert = new AlertViewModel();
    readonly name = ko.observable('');
}