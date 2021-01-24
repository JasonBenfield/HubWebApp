import * as ko from 'knockout';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { AlertViewModel } from 'XtiShared/Alert';
import * as template from './ListCard.html';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';

export class ListCardViewModel extends ComponentViewModel {
    constructor(componentTemplate: IComponentTemplate = null) {
        super(componentTemplate || new ComponentTemplate('list-card', template));
    }

    readonly alert = new AlertViewModel();
    readonly title = ko.observable('');
    readonly items = ko.observableArray<any>([]);
    readonly hasItems = ko.observable(false);
}