import { InputLabel, MenuItem, FormControl, Select } from '@mui/material';

export function BasicSelect({ label, items, value, onChange }) {
  return (
    <FormControl fullWidth>
      <InputLabel>{label}</InputLabel>
      <Select label={label} value={value} onChange={onChange}>
        {items.map((item) => (
          <MenuItem key={item} value={item}>
            {item}
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );
}
