export const getStackedColumns = (directives, gridConfig) => {
    const columns = [];
    const stackedColumns = {};
    directives.forEach((configKey) => {
      const {
        sortable,
        filterable,
        resizable,
        autofit,
        columnWidth,
        show,
        stackedHeader,
        format,
        aliased,
        aliasedHeader,
        showmenu,
        columnMaxWidth,
        columnMinWidth,
        orderable,
      } = gridConfig[configKey];
  
      const newColumn = {
        allowSorting: sortable,
        allowFiltering: filterable,
        allowResizing: resizable,
        showColumnMenu: showmenu,
        allowReordering: orderable,
        width: columnWidth,
        format:format,
        maxWidth: columnMaxWidth,
        minWidth: columnMinWidth,
        autoFit: autofit,
        field: configKey,
        visible: configKey !== 'ROWNUM_' && show,
        headerText: aliased ? aliasedHeader : configKey,
        columns: undefined,
      };
  
      if (stackedHeader && stackedHeader !== '') {
        const indexOfStacked = [...columns].findIndex(
          (col) => col.headerText === stackedHeader
        );
        if (indexOfStacked === -1) {
          columns.push({
            headerText: stackedHeader,
            columns: [newColumn],
          });
        } else {
          const stackedColumn = columns.splice(indexOfStacked, 1)[0];
          columns.push({
            ...stackedColumn,
            columns: [...stackedColumn.columns, newColumn],
          });
        }
      } else {
        columns.push(newColumn);
      }
    });
  
    const toReturn = [];
  
    columns.forEach((column) => {
      toReturn.push(column);
    });
    Object.keys(stackedColumns).forEach((key) => {
      toReturn.push({ field: key, columns: [...stackedColumns[key]] });
    });
    console.log('checking the result', JSON.stringify(toReturn));
    return toReturn;
  };
  