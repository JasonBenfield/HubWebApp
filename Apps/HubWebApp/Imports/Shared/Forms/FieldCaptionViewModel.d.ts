import * as ko from 'knockout';
export declare class FieldCaptionViewModel implements IFieldCaptionViewModel {
    readonly forControl: ko.Observable<string>;
    readonly caption: ko.Observable<string>;
    readonly css: ko.Observable<string>;
    readonly isVisible: ko.Observable<boolean>;
}
