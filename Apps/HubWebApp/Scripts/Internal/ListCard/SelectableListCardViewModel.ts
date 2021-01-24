import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import * as template from './SelectableListCard.html';
import { DefaultEvent } from 'XtiShared/Events';
import { ListCardViewModel } from './ListCardViewModel';

export class SelectableListCardViewModel extends ListCardViewModel {
    constructor() {
        super(new ComponentTemplate('selectable-list-card', template));
    }

    private readonly _itemSelected = new DefaultEvent<any>(this);
    readonly itemSelected = this._itemSelected.handler();

    select(item: any) {
        this._itemSelected.invoke(item);
        return true;
    }
}