import { HtmlComponentViewModel } from "./HtmlComponentViewModel";
import { AggregateComponentViewModel } from "./AggregateComponentViewModel";
import * as ko from 'knockout';
export declare class FormComponentViewModel extends HtmlComponentViewModel {
    readonly content: AggregateComponentViewModel;
    constructor(content?: AggregateComponentViewModel);
    readonly action: ko.Observable<string>;
    readonly method: ko.Observable<string>;
    readonly autocomplete: ko.Observable<string>;
    private readonly _submitted;
    readonly submitted: import("../Events").DefaultEventHandler<any>;
    private isDefaultSubmit;
    useDefaultSubmit(): void;
    submit(_: any, event: any): Promise<boolean>;
}
