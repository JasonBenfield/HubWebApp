export declare class ContextualClass {
    private readonly value;
    static readonly default: ContextualClass;
    static readonly success: ContextualClass;
    static readonly info: ContextualClass;
    static readonly warning: ContextualClass;
    static readonly danger: ContextualClass;
    static readonly primary: ContextualClass;
    static readonly secondary: ContextualClass;
    static readonly light: ContextualClass;
    static readonly dark: ContextualClass;
    private constructor();
    readonly isDefault: boolean;
    readonly isSuccess: boolean;
    readonly isInfo: boolean;
    readonly isWarning: boolean;
    readonly isDanger: boolean;
    readonly isPrimary: boolean;
    readonly isSecondary: boolean;
    readonly isLight: boolean;
    readonly isDark: boolean;
    append(prefix: string): string;
    equals(other: ContextualClass | string): boolean;
    toString(): string;
}
