import * as ko from 'knockout';
import * as template from './Panel.html';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';

export class PanelViewModel<TContent> extends ComponentViewModel {
    constructor(public readonly content: TContent) {
        super(new ComponentTemplate('panel', template));
    }

    readonly isActive = ko.observable(false);
}