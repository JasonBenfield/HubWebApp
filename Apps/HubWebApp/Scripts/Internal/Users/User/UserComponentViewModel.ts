import { ComponentTemplate } from "XtiShared/ComponentTemplate";
import { ComponentViewModel } from "XtiShared/ComponentViewModel";
import { AlertViewModel } from "XtiShared/Alert";
import * as template from './UserComponent.html';
import * as ko from 'knockout';

export class UserComponentViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('user-component', template));
    }

    readonly alert = new AlertViewModel();
    readonly userName = ko.observable('');
    readonly name = ko.observable('');
    readonly email = ko.observable('');
}