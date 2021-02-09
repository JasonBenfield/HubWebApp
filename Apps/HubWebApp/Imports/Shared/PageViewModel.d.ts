import { ComponentViewModel } from './ComponentViewModel';
import { ModalErrorComponentViewModel } from './Error/ModalErrorComponentViewModel';
import { AggregateComponentViewModel } from './Html/AggregateComponentViewModel';
export declare class PageViewModel extends ComponentViewModel implements IPageViewModel {
    constructor();
    readonly content: AggregateComponentViewModel;
    readonly modalError: ModalErrorComponentViewModel;
}
