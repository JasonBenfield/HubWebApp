export declare class FieldViewValue {
    private value;
    getValue(): any;
    setValue(value: any): void;
    setValueFromView(viewValue: any): any;
    protected _fromView(value: any): any;
    toView(): any;
    protected _toView(value: any): any;
}
