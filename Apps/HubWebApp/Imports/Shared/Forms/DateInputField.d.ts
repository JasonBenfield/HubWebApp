import { DateConstraintCollection } from "./ConstraintCollection";
import { InputFieldViewModel } from "./InputFieldViewModel";
import { SimpleField } from "./SimpleField";
export declare class DateInputField extends SimpleField {
    constructor(prefix: string, name: string, vm: InputFieldViewModel, viewValue?: any);
    private readonly inputVM;
    readonly constraints: DateConstraintCollection;
    setFocus(): void;
    blur(): void;
    setValue(value: Date): void;
    getValue(): Date;
}
