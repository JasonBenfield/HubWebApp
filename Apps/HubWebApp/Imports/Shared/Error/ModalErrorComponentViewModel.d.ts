import * as ko from 'knockout';
import { ModalOptionsViewModel } from '../ModalOptionsViewModel';
import { ModalErrorViewModel } from './ModalErrorViewModel';
import { DefaultEventHandler } from '../Events';
import { ErrorModel } from '../ErrorModel';
import { ComponentViewModel } from '../ComponentViewModel';
import { ButtonViewModel } from '../Html/ButtonViewModel';
export declare class ModalErrorComponentViewModel extends ComponentViewModel {
    constructor();
    readonly title: ko.Observable<string>;
    readonly isVisible: ko.Observable<boolean>;
    readonly modalOptions: ModalOptionsViewModel;
    readonly errors: ko.ObservableArray<ModalErrorViewModel>;
    private readonly errorSelectedEvents;
    readonly okCommand: ButtonViewModel;
    private readonly _errorSelected;
    readonly errorSelected: DefaultEventHandler<ErrorModel>;
    private onErrorSelected;
}
