import { ComponentViewModel } from "../ComponentViewModel";
import * as ko from 'knockout';
export declare class HtmlComponentViewModel extends ComponentViewModel implements IHtmlComponentViewModel {
    constructor(template: IComponentTemplate);
    readonly id: ko.Observable<string>;
    readonly name: ko.Observable<string>;
    readonly css: ko.Observable<string>;
    readonly isVisible: ko.Observable<boolean>;
    readonly title: ko.Observable<string>;
}
