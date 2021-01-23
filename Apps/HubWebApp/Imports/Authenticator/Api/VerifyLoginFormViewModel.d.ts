import * as ko from 'knockout';
import { InputFieldViewModel } from "XtiShared/Forms/InputFieldViewModel";
export declare class VerifyLoginFormViewModel {
    readonly componentName: ko.Observable<string>;
    readonly UserName: InputFieldViewModel;
    readonly Password: InputFieldViewModel;
}
