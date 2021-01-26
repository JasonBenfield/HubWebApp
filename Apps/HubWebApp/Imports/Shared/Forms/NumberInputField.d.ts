import { NumberConstraintCollection } from "./ConstraintCollection";
import { InputFieldViewModel } from "./InputFieldViewModel";
import { SimpleField } from "./SimpleField";
export declare class NumberInputField extends SimpleField {
    constructor(prefix: string, name: string, vm: InputFieldViewModel, viewValue?: any);
    private readonly inputVM;
    readonly constraints: NumberConstraintCollection;
    setFocus(): void;
    blur(): void;
    setValue(value: number): void;
    getValue(): number;
    protect(): void;
}
