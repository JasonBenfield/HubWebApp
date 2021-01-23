import { VerifyLoginFormViewModel } from "./VerifyLoginFormViewModel";
import { Form } from 'XtiShared/Forms/Form';
export declare class VerifyLoginForm extends Form {
    private readonly vm;
    constructor(vm: VerifyLoginFormViewModel);
    readonly UserName: import("../../../Imports/Shared/Forms/TextInputField").TextInputField;
    readonly Password: import("../../../Imports/Shared/Forms/TextInputField").TextInputField;
}
