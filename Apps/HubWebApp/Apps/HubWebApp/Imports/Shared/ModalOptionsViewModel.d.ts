import * as ko from 'knockout';
import { DefaultEventHandler } from './Events';
export declare class ModalOptionsViewModel {
    readonly command: ko.Observable<"" | "hide" | "show">;
    private readonly _closed;
    readonly closed: DefaultEventHandler<any>;
    handleClose(): void;
}
