import { TextConstraintCollection } from "./ConstraintCollection";
import { InputFieldViewModel } from "./InputFieldViewModel";
import { SimpleField } from "./SimpleField";
export declare class TextInputField extends SimpleField {
    constructor(prefix: string, name: string, vm: InputFieldViewModel, viewValue?: any);
    private readonly inputVM;
    readonly constraints: TextConstraintCollection;
    protect(): void;
    setFocus(): void;
    blur(): void;
    setValue(value: string): void;
    getValue(): string;
    setMaxLength(maxLength: number): void;
}
