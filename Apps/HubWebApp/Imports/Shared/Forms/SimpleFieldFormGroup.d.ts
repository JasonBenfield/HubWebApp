import { ErrorModel } from "../ErrorModel";
import { BlockViewModel } from "../Html/BlockViewModel";
import { ErrorList } from "./ErrorList";
import { FormGroup } from "../Html/FormGroup";
export declare abstract class SimpleFieldFormGroup<TValue> extends FormGroup implements IField {
    constructor(prefix: string, name: string, vm?: BlockViewModel);
    private readonly name;
    getName(): string;
    abstract getValue(): TValue;
    abstract setValue(value: TValue): any;
    private readonly dropdown;
    private readonly alertList;
    getField(name: string): this;
    setErrors(errors: ErrorModel[]): void;
    clearErrors(): void;
    validate(errors: IErrorList): void;
    protected abstract validateConstraints(fieldErrors: ErrorList): any;
    import(values: Record<string, any>): void;
    export(values: Record<string, any>): void;
}
