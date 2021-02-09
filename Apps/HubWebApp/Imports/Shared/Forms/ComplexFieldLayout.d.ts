import { BaseForm } from "./BaseForm";
import { ComplexFieldFormGroup } from "./ComplexFieldFormGroup";
export declare class ComplexFieldLayout {
    private readonly complexField;
    constructor(complexField: ComplexFieldFormGroup | BaseForm);
    execute(): void;
    protected executeLayout(complexField: ComplexFieldFormGroup | BaseForm): void;
}
